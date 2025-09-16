using System.Security.Claims;

using Microsoft.AspNetCore.SignalR;


namespace ChatApp.API.Auth
{
    public class JwtUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        => connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? connection.User?.FindFirst("sub")?.Value;
    }
}