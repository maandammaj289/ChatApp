using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Domain.Entities;


namespace ChatApp.Application.Interfaces
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message, CancellationToken ct = default);
        Task<Message?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task UpdateAsync(Message message, CancellationToken ct = default);
        Task<IReadOnlyList<Message>> GetConversationAsync(string userId, string peerId, int skip, int take, CancellationToken ct = default);
        Task<IReadOnlyList<Message>> GetGroupMessagesAsync(Guid groupId, int skip, int take, CancellationToken ct = default);
        Task<int> GetUnreadCountAsync(string userId, CancellationToken ct = default);
    }
}