using ChatApp.Application.Auth.DTOs;
using ChatApp.Domain.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Infrastructure.Services
{
    public static class JwtHelper
    {

        public static LoginResultDto GenerateToken(AppUser user)
        {
            // يمكنك استخدام IConfiguration بدلاً من قيم ثابتة
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("K1rVbG4kUx5c7r0p9t3M+V9kZW9a8fJmzH5gP0eD5Jo=!"));
            var signingCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

     

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: "chatapp",
                audience: "chatapp",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
            );

            return new LoginResultDto
            {
                Username = user.UserName ?? "",
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
