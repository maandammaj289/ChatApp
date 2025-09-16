using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Persistence
{

    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _ctx;
        public GroupRepository(AppDbContext ctx) { _ctx = ctx; }


        public async Task<ChatGroup> AddAsync(ChatGroup group, CancellationToken ct = default)
        { await _ctx.Groups.AddAsync(group, ct); return group; }


        public Task<ChatGroup?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _ctx.Groups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == id, ct)!;


        public async Task AddMemberAsync(GroupMember member, CancellationToken ct = default)
        { await _ctx.GroupMembers.AddAsync(member, ct); }


        public Task<bool> IsMemberAsync(Guid groupId, string userId, CancellationToken ct = default)
        => _ctx.GroupMembers.AnyAsync(m => m.GroupId == groupId && m.UserId == userId, ct);
    }
}