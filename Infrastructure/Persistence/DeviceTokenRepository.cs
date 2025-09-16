using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Persistence
{
    public class DeviceTokenRepository : IDeviceTokenRepository
    {
        private readonly AppDbContext _ctx;
        public DeviceTokenRepository(AppDbContext ctx) { _ctx = ctx; }


        public async Task UpsertAsync(DeviceToken token, CancellationToken ct = default)
        {
            var existing = await _ctx.DeviceTokens.FirstOrDefaultAsync(x => x.Token == token.Token, ct);
            if (existing == null) await _ctx.DeviceTokens.AddAsync(token, ct);
            else { existing.UserId = token.UserId; existing.Platform = token.Platform; }
        }
    }
}