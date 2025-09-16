using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Persistence
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _ctx;
        public MessageRepository(AppDbContext ctx) { _ctx = ctx; }


        public async Task AddAsync(Message message, CancellationToken ct = default)
        {
            await _ctx.Messages.AddAsync(message, ct);
        }
        public Task<Message?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _ctx.Messages.FirstOrDefaultAsync(m => m.Id == id, ct)!;
        public Task UpdateAsync(Message message, CancellationToken ct = default)
        { _ctx.Messages.Update(message); return Task.CompletedTask; }


        public async Task<IReadOnlyList<Message>> GetConversationAsync(string userId, string peerId, int skip, int take, CancellationToken ct = default)
        {
            return await _ctx.Messages
            .Where(m => (m.SenderId == userId && m.ReceiverUserId == peerId) || (m.SenderId == peerId && m.ReceiverUserId == userId))
            .OrderByDescending(m => m.SentAt)
            .Skip(skip).Take(take)
            .AsNoTracking()
            .ToListAsync(ct);
        }
        public async Task<IReadOnlyList<Message>> GetGroupMessagesAsync(Guid groupId, int skip, int take, CancellationToken ct = default)
        {
            return await _ctx.Messages
            .Where(m => m.ReceiverGroupId == groupId)
            .OrderByDescending(m => m.SentAt)
            .Skip(skip).Take(take)
            .AsNoTracking()
            .ToListAsync(ct);
        }
        public Task<int> GetUnreadCountAsync(string userId, CancellationToken ct = default)
        {
            return _ctx.Messages.CountAsync(m => m.ReceiverUserId == userId && m.ReadAt == null, ct);
        }
    }
}