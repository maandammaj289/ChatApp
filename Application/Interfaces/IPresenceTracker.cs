using System.Threading.Tasks;


namespace ChatApp.Application.Interfaces
{
    public interface IPresenceTracker
    {
        Task AddConnectionAsync(string userId, string connectionId);
        Task RemoveConnectionAsync(string userId, string connectionId);
        Task<bool> IsConnected(string userId);
    }
}