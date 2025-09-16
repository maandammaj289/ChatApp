using System;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;


namespace ChatApp.Application.Devices.Commands
{
    public class RegisterDeviceTokenCommand
    {
        public string UserId { get; init; } = string.Empty;
        public string Token { get; init; } = string.Empty;
        public string Platform { get; init; } = "unknown";
    }


    public class RegisterDeviceTokenCommandHandler
    {
        private readonly IDeviceTokenRepository _repo;
        private readonly IUnitOfWork _uow;
        public RegisterDeviceTokenCommandHandler(IDeviceTokenRepository repo, IUnitOfWork uow)
        { _repo = repo; _uow = uow; }


        public async Task Handle(RegisterDeviceTokenCommand req, CancellationToken ct = default)
        {
            await _repo.UpsertAsync(new DeviceToken { UserId = req.UserId, Token = req.Token, Platform = req.Platform }, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }
}