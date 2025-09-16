using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Domain.Entities;


namespace ChatApp.Application.Interfaces
{
    public interface IGroupRepository
    {
        Task<ChatGroup> AddAsync(ChatGroup group, CancellationToken ct = default);
        Task<ChatGroup?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddMemberAsync(GroupMember member, CancellationToken ct = default);
        Task<bool> IsMemberAsync(Guid groupId, string userId, CancellationToken ct = default);
    }
}