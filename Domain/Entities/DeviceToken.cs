using System;


namespace ChatApp.Domain.Entities
{
    public class DeviceToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // FCM token
        public string Platform { get; set; } = "unknown"; // ios/android/web
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}