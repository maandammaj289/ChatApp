using System.Threading.Tasks;


namespace ChatApp.Application.Interfaces
{
    public interface IFirebaseService
    {
        Task SendToUserAsync(string userId, string title, string body, object? data = null);
    }
}