using System;
using System.Security.Claims;
using System.Threading.Tasks;

using ChatApp.Application.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace ChatApp.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IPresenceTracker _presence;
        public ChatHub(IPresenceTracker presence) { _presence = presence; }


        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier!;
            await _presence.AddConnectionAsync(userId, Context.ConnectionId);
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier!;
            await _presence.RemoveConnectionAsync(userId, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }


        public Task JoinGroup(string groupName) => Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        public Task LeaveGroup(string groupName) => Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}