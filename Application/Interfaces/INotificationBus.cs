using System.Threading.Tasks;


namespace ChatApp.Application.Interfaces
{
    public interface INotificationBus
    {
        Task SendRealtimeToUser(string userId, string method, object payload);
        Task SendRealtimeToGroup(string groupName, string method, object payload);
        Task QueuePushToUser(string userId, string title, string body, object? data = null);
        Task<bool> IsUserConnected(string userId);
    }
}