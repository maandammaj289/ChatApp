using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;


namespace ChatApp.Application.Groups.Commands
{
    public class CreateGroupCommand
    {
        public string Name { get; init; } = string.Empty;
        public List<string> MemberUserIds { get; init; } = new();
    }


    public class CreateGroupCommandHandler
    {
        private readonly IGroupRepository _groups;
        private readonly IUnitOfWork _uow;
        public CreateGroupCommandHandler(IGroupRepository groups, IUnitOfWork uow)
        { _groups = groups; _uow = uow; }


        public async Task<Guid> Handle(CreateGroupCommand req, CancellationToken ct = default)
        {
            var group = new ChatGroup { Name = req.Name };
            foreach (var uid in req.MemberUserIds)
                group.Members.Add(new GroupMember { GroupId = group.Id, UserId = uid });


            await _groups.AddAsync(group, ct);
            await _uow.SaveChangesAsync(ct);
            return group.Id;
        }
    }
}