using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;


namespace ChatApp.Application.Messages.Queries
{
    public class GetConversationHistoryQuery
    {
        public string UserId { get; init; } = string.Empty;
        public string PeerId { get; init; } = string.Empty;
        public int Skip { get; init; } = 0;
        public int Take { get; init; } = 50;
    }


    public class GetConversationHistoryQueryHandler
    {
        private readonly IMessageRepository _messages;
        public GetConversationHistoryQueryHandler(IMessageRepository messages)
        { _messages = messages; }


        public Task<IReadOnlyList<Message>> Handle(GetConversationHistoryQuery req, CancellationToken ct = default)
        => _messages.GetConversationAsync(req.UserId, req.PeerId, req.Skip, req.Take, ct);
    }
}