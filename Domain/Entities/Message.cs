using System;

using ChatApp.Domain.Enums;


namespace ChatApp.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SenderId { get; set; } = string.Empty;
        public string? ReceiverUserId { get; set; }
        public Guid? ReceiverGroupId { get; set; }
        public string? Content { get; set; }
        public string? AttachmentUrl { get; set; }
        public MessageType Type { get; set; } = MessageType.Text;
        public MessageStatus Status { get; set; } = MessageStatus.Sent;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveredAt { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}