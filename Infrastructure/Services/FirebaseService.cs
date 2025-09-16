using System.Linq;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;


namespace ChatApp.Infrastructure.Services
{
    // Stub: Replace with Firebase Admin SDK implementation
    public class FirebaseService : IFirebaseService
    {
        private readonly AppDbContext _ctx;
        public FirebaseService(AppDbContext ctx) { _ctx = ctx; }


        public async Task SendToUserAsync(string userId, string title, string body, object? data = null)
        {
            var tokens = await _ctx.DeviceTokens.Where(t => t.UserId == userId).Select(t => t.Token).ToListAsync();
            // TODO: Use Firebase SDK to push to tokens
            // This is a placeholder to keep architecture clean.
            System.Console.WriteLine($"[FCM] to {userId} ({tokens.Count} tokens): {title} - {body}");
        }
    }
}