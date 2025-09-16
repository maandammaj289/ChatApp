using ChatApp.Application.Auth.DTOs;

namespace ChatApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterRequestDto request);
        Task<LoginResultDto?> LoginAsync(string username, string password);
   

    }
}
