using System;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Application.Messages.Commands;
using ChatApp.Application.Messages.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ChatApp.API.Controllers
{
[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class MessagesController : ControllerBase
{
private readonly SendMessageCommandHandler _send;
private readonly MarkDeliveredCommandHandler _delivered;
private readonly MarkReadCommandHandler _read;
private readonly GetConversationHistoryQueryHandler _history;


public MessagesController(
SendMessageCommandHandler send,
MarkDeliveredCommandHandler delivered,
MarkReadCommandHandler read,
GetConversationHistoryQueryHandler history)
{ _send = send; _delivered = delivered; _read = read; _history = history; }


[HttpPost]
        public async Task<IActionResult> Send([FromBody] SendMessageCommand cmd, CancellationToken ct)
        {
            var userId = User.FindFirst("sub")?.Value ?? User.Identity!.Name!;
            cmd = new SendMessageCommand
            {
                SenderId = userId,
                ReceiverUserId = cmd.ReceiverUserId,
                ReceiverGroupId = cmd.ReceiverGroupId,
                Type = cmd.Type,
                Content = cmd.Content,
                AttachmentUrl = cmd.AttachmentUrl
            };
            var id = await _send.Handle(cmd, ct);
            return Ok(new { messageId = id });
        }


[HttpPost("{id:guid}/delivered")]
public async Task<IActionResult> Delivered(Guid id, CancellationToken ct)
{ await _delivered.Handle(new MarkDeliveredCommand { MessageId = id }, ct); return NoContent(); }


[HttpPost("{id:guid}/read")]
public async Task<IActionResult> Read(Guid id, CancellationToken ct)
{ await _read.Handle(new MarkReadCommand { MessageId = id }, ct); return NoContent(); }


[HttpGet("history/{peerId}")]
public async Task<IActionResult> History(string peerId, int skip = 0, int take = 50, CancellationToken ct = default)
{
var userId = User.FindFirst("sub")?.Value ?? User.Identity!.Name!;
var list = await _history.Handle(new GetConversationHistoryQuery { UserId = userId, PeerId = peerId, Skip = skip, Take = take }, ct);
return Ok(list);
}
}
}