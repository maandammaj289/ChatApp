//using System.Threading.Tasks;
//using ChatApp.Application.Interfaces;
//using ChatApp.API.Hubs;
//using Microsoft.AspNetCore.SignalR;
//using WebAPI.Hubs;


//namespace ChatApp.Infrastructure.Services
//{
//    public class SignalRNotificationBus : INotificationBus
//    {
//        private readonly IHubContext<ChatHub> _hub;
//        private readonly IFirebaseService _firebase;
//        private readonly IPresenceTracker _presence;
//        public SignalRNotificationBus(IHubContext<ChatHub> hub, IFirebaseService firebase, IPresenceTracker presence)
//        { _hub = hub; _firebase = firebase; _presence = presence; }


//        public Task SendRealtimeToUser(string userId, string method, object payload)
//        => _hub.Clients.User(userId).SendAsync(method, payload);


//        public Task SendRealtimeToGroup(string groupName, string method, object payload)
//        => _hub.Clients.Group(groupName).SendAsync(method, payload);


//        public Task<bool> IsUserConnected(string userId) => _presence.IsConnected(userId);


//        public Task QueuePushToUser(string userId, string title, string body, object? data = null)
//        => _firebase.SendToUserAsync(userId, title, body, data);
//    }
//}