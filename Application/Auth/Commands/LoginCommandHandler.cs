using ChatApp.Application.Auth.DTOs;
using ChatApp.Application.Interfaces;

using MediatR;

namespace ChatApp.Application.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDto?>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<LoginResultDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return _authService.LoginAsync(request.Username, request.Password);
        }
    }
}
