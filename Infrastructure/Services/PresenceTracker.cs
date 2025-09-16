using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;


namespace ChatApp.Infrastructure.Services
{
    public class PresenceTracker : IPresenceTracker
    {
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, byte>> _online = new();


        public Task AddConnectionAsync(string userId, string connectionId)
        {
            var set = _online.GetOrAdd(userId, _ => new ConcurrentDictionary<string, byte>());
            set.TryAdd(connectionId, 0);
            return Task.CompletedTask;
        }


        public Task RemoveConnectionAsync(string userId, string connectionId)
        {
            if (_online.TryGetValue(userId, out var set))
            {
                set.TryRemove(connectionId, out _);
                if (!set.Any()) _online.TryRemove(userId, out _);
            }
            return Task.CompletedTask;
        }


        public Task<bool> IsConnected(string userId)
        => Task.FromResult(_online.ContainsKey(userId));
    }
}