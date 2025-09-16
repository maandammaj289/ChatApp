using System;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Enums;


namespace ChatApp.Application.Messages.Commands
{
    public class SendMessageCommand
    {
        public string SenderId { get; init; } = string.Empty;
        public string? ReceiverUserId { get; init; }
        public Guid? ReceiverGroupId { get; init; }
        public MessageType Type { get; init; } = MessageType.Text;
        public string? Content { get; init; }
        public string? AttachmentUrl { get; init; }
    }


    public class SendMessageCommandHandler
    {
        private readonly IMessageRepository _messages;
        private readonly INotificationBus _bus;
        private readonly IUnitOfWork _uow;


        public SendMessageCommandHandler(IMessageRepository messages, INotificationBus bus, IUnitOfWork uow)
        {
            _messages = messages; _bus = bus; _uow = uow;
        }


        public async Task<Guid> Handle(SendMessageCommand req, CancellationToken ct = default)
        {
            var msg = new Message
            {
                SenderId = req.SenderId,
                ReceiverUserId = req.ReceiverUserId,
                ReceiverGroupId = req.ReceiverGroupId,
                Type = req.Type,
                Content = req.Content,
                AttachmentUrl = req.AttachmentUrl,
                Status = MessageStatus.Sent,
                SentAt = DateTime.UtcNow
            };


            await _messages.AddAsync(msg, ct);
            await _uow.SaveChangesAsync(ct);


            // Realtime delivery (and push if needed) via NotificationBus
            if (msg.ReceiverUserId != null)
            {
                if (await _bus.IsUserConnected(msg.ReceiverUserId))
                    await _bus.SendRealtimeToUser(msg.ReceiverUserId, "ReceiveMessage", msg);
                else
                    await _bus.QueuePushToUser(msg.ReceiverUserId, "رسالة جديدة", "لديك رسالة جديدة", new { messageId = msg.Id });
            }
            else if (msg.ReceiverGroupId != null)
            {
                var groupName = msg.ReceiverGroupId.ToString();
                await _bus.SendRealtimeToGroup(groupName, "ReceiveMessage", msg);
                // (اختياري) Push لأعضاء غير متصلين يمكن تنفيذه في البنية التحتية
            }


            return msg.Id;
        }
    }
}