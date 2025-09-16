using System;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;


namespace ChatApp.Application.Groups.Commands
{
    public class AddMemberCommand
    {
        public Guid GroupId { get; init; }
        public string UserId { get; init; } = string.Empty;
    }


    public class AddMemberCommandHandler
    {
        private readonly IGroupRepository _groups;
        private readonly IUnitOfWork _uow;
        public AddMemberCommandHandler(IGroupRepository groups, IUnitOfWork uow)
        { _groups = groups; _uow = uow; }


        public async Task Handle(AddMemberCommand req, CancellationToken ct = default)
        {
            var member = new GroupMember { GroupId = req.GroupId, UserId = req.UserId };
            await _groups.AddMemberAsync(member, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }
}