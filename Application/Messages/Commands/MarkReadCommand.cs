using System;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;
using ChatApp.Domain.Enums;


namespace ChatApp.Application.Messages.Commands
{
    public class MarkReadCommand
    {
        public Guid MessageId { get; init; }
    }


    public class MarkReadCommandHandler
    {
        private readonly IMessageRepository _messages;
        private readonly INotificationBus _bus;
        private readonly IUnitOfWork _uow;
        public MarkReadCommandHandler(IMessageRepository messages, INotificationBus bus, IUnitOfWork uow)
        { _messages = messages; _bus = bus; _uow = uow; }


        public async Task Handle(MarkReadCommand req, CancellationToken ct = default)
        {
            var msg = await _messages.GetByIdAsync(req.MessageId, ct);
            if (msg == null) return;
            if (msg.Status < MessageStatus.Read)
            {
                msg.Status = MessageStatus.Read;
                msg.ReadAt = DateTime.UtcNow;
                await _messages.UpdateAsync(msg, ct);
                await _uow.SaveChangesAsync(ct);
                await _bus.SendRealtimeToUser(msg.SenderId, "MessageRead", new { msg.Id, msg.ReadAt });
            }
        }
    }
}