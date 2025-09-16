using Application.Auth.DTOs;
using ChatApp.Application.Auth.DTOs;

using MediatR;

namespace ChatApp.Application.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResultDto?>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
