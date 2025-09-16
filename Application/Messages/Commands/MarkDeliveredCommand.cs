using System;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;
using ChatApp.Domain.Enums;


namespace ChatApp.Application.Messages.Commands
{
    public class MarkDeliveredCommand
    {
        public Guid MessageId { get; init; }
    }


    public class MarkDeliveredCommandHandler
    {
        private readonly IMessageRepository _messages;
        private readonly IUnitOfWork _uow;
        public MarkDeliveredCommandHandler(IMessageRepository messages, IUnitOfWork uow)
        { _messages = messages; _uow = uow; }


        public async Task Handle(MarkDeliveredCommand req, CancellationToken ct = default)
        {
            var msg = await _messages.GetByIdAsync(req.MessageId, ct);
            if (msg == null) return;
            if (msg.Status < MessageStatus.Delivered)
            {
                msg.Status = MessageStatus.Delivered;
                msg.DeliveredAt = DateTime.UtcNow;
                await _messages.UpdateAsync(msg, ct);
                await _uow.SaveChangesAsync(ct);
            }
        }
    }
}