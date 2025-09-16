using ChatApp.Application.Auth.DTOs;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Persistence;
using ChatApp.Infrastructure.Services;

using Microsoft.AspNetCore.Identity;

using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly INotificationBus _notificationBus; // خدمة الإشعارات
    private readonly AppDbContext _dbContext;

    public AuthService(UserManager<AppUser> userManager, INotificationBus notificationBus, AppDbContext dbContext)
    {
        _userManager = userManager;
        _notificationBus = notificationBus;
        _dbContext = dbContext;
    }
    public async Task<LoginResultDto?> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null) return null;

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result) return null;

        return JwtHelper.GenerateToken(user);
    }

    public async Task<bool> RegisterAsync(RegisterRequestDto request)
    {
        var exists = await _userManager.FindByNameAsync(request.Username);
        if (exists != null) return false;

        var user = new AppUser { UserName = request.Username, DisplayName = request.Username };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded) return false;

        // إرسال رسالة ترحيبية عبر Outbox + SignalR
        await SendWelcomeMessage(user);

        return true;
    }

    private async Task SendWelcomeMessage(AppUser user)
    {
        var payload = new
        {
            Content = $"Welcome {user.DisplayName}!",
            SentAt = DateTime.UtcNow
        };

        // إرسال مباشرة للمستخدم الحالي إذا متصل (Presence)
        if (await _notificationBus.IsUserConnected(user.Id))
        {
            await _notificationBus.SendRealtimeToUser(user.Id, "ReceiveMessage", payload);
        }

        // أو أضف للـOutbox لإرسال لاحقًا
        await _dbContext.Outbox.AddAsync(new OutboxMessage
        {
            Type = "WelcomeMessage",
            Payload = JsonSerializer.Serialize(payload),
            CreatedAt = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();
    }
}
