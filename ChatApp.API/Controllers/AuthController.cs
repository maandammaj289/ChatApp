using ChatApp.Application.Interfaces;
using ChatApp.Application.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var success = await _authService.RegisterAsync(request);
            if (!success) return BadRequest("Username already exists");
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.LoginAsync(request.Username, request.Password);
            if (result == null) return Unauthorized();
            return Ok(result);
        }
    }
}
