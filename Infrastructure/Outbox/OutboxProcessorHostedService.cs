using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;


namespace ChatApp.Infrastructure.Outbox
{
    public class OutboxProcessorHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public OutboxProcessorHostedService(IServiceScopeFactory scopeFactory) { _scopeFactory = scopeFactory; }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var bus = scope.ServiceProvider.GetRequiredService<INotificationBus>();


                    var batch = await ctx.Outbox.Where(o => o.ProcessedAt == null)
                    .OrderBy(o => o.CreatedAt).Take(50).ToListAsync(stoppingToken);


                    foreach (var ob in batch)
                    {
                        if (ob.Type == "MessageCreated")
                        {
                            var payload = JsonDocument.Parse(ob.Payload).RootElement;
                            var messageId = payload.GetProperty("messageId").GetGuid();
                            var msg = await ctx.Messages.FirstOrDefaultAsync(m => m.Id == messageId, stoppingToken);
                            if (msg != null)
                            {
                                if (msg.ReceiverUserId != null)
                                {
                                    if (await bus.IsUserConnected(msg.ReceiverUserId))
                                        await bus.SendRealtimeToUser(msg.ReceiverUserId, "ReceiveMessage", msg);
                                    else
                                        await bus.QueuePushToUser(msg.ReceiverUserId, "رسالة جديدة", "لديك رسالة جديدة", new { messageId = msg.Id });
                                }
                                else if (msg.ReceiverGroupId != null)
                                {
                                    await bus.SendRealtimeToGroup(msg.ReceiverGroupId.ToString()!, "ReceiveMessage", msg);
                                }
                            }
                        }
                        ob.ProcessedAt = DateTime.UtcNow;
                        ob.Attempts++;
                    }
                    if (batch.Any())
                        await ctx.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Outbox error: {ex.Message}");
                }


                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}