using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;


namespace ChatApp.Application.Messages.Queries
{
    public class GetUnreadCountQuery
    {
        public string UserId { get; init; } = string.Empty;
    }


    public class GetUnreadCountQueryHandler
    {
        private readonly IMessageRepository _messages;
        public GetUnreadCountQueryHandler(IMessageRepository messages) { _messages = messages; }
        public Task<int> Handle(GetUnreadCountQuery req, CancellationToken ct = default)
        => _messages.GetUnreadCountAsync(req.UserId, ct);
    }
}