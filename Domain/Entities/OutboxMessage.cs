using System;


namespace ChatApp.Domain.Entities
{
    public class OutboxMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = string.Empty; // e.g., MessageCreated
        public string Payload { get; set; } = string.Empty; // JSON
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }
        public int Attempts { get; set; } = 0;
    }
}