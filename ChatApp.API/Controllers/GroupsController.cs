using System;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Groups.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ChatApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly CreateGroupCommandHandler _create;
        private readonly AddMemberCommandHandler _add;
        public GroupsController(CreateGroupCommandHandler create, AddMemberCommandHandler add)
        { _create = create; _add = add; }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupCommand cmd, CancellationToken ct)
        { var id = await _create.Handle(cmd, ct); return Ok(new { groupId = id }); }


        [HttpPost("{groupId:guid}/members")]
        public async Task<IActionResult> Add(Guid groupId, [FromBody] string userId, CancellationToken ct)
        { await _add.Handle(new AddMemberCommand { GroupId = groupId, UserId = userId }, ct); return NoContent(); }
    }
}