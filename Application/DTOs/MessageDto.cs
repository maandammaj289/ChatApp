using System;

using ChatApp.Domain.Enums;


namespace ChatApp.Application.DTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string? ReceiverUserId { get; set; }
        public Guid? ReceiverGroupId { get; set; }
        public string? Content { get; set; }
        public string? AttachmentUrl { get; set; }
        public MessageType Type { get; set; }
        public MessageStatus Status { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}