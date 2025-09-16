using ChatApp.API.Auth;
using ChatApp.API.Services;
using ChatApp.Application.Auth.Commands;
using ChatApp.Application.Devices.Commands;
using ChatApp.Application.Groups.Commands;
using ChatApp.Application.Interfaces;
using ChatApp.Application.Messages.Commands;
using ChatApp.Application.Messages.Queries;
using ChatApp.Infrastructure.Outbox;
using ChatApp.Infrastructure.Persistence;
using ChatApp.Infrastructure.Services;

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace ChatApp.API.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddChatApp(this IServiceCollection services, IConfiguration config)
        {



            // Repositories & UoW
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IDeviceTokenRepository, DeviceTokenRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // Services
            services.AddSingleton<IPresenceTracker, PresenceTracker>();
            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<INotificationBus, SignalRNotificationBus>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IAuthService, AuthService>();

            // Command/Query handlers (simple DI style)
            services.AddScoped<SendMessageCommandHandler>();
            services.AddScoped<MarkDeliveredCommandHandler>();
            services.AddScoped<MarkReadCommandHandler>();
            services.AddScoped<CreateGroupCommandHandler>();
            services.AddScoped<AddMemberCommandHandler>();
            services.AddScoped<RegisterDeviceTokenCommandHandler>();
            services.AddScoped<GetConversationHistoryQueryHandler>();
            services.AddScoped<GetUnreadCountQueryHandler>();
            services.AddScoped<LoginCommandHandler>();



            // SignalR
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, JwtUserIdProvider>();


            // Hosted Services
            services.AddHostedService<OutboxProcessorHostedService>();


            return services;
        }
    }
}