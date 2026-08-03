using ECommerceG03.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceG03.Infrastructure.Identity.Services
{
    public class TokenSerivce : ITokenService
    {
        private readonly JwtSettings _jwtOptions;

        public TokenSerivce(IOptions<JwtSettings> jwtOptions) 
        {
            _jwtOptions = jwtOptions.Value;
        }
        // JwtToken Bearer: Implement Token Service
        // Claims [Payload] => UserId, Email, UserName, Roles
        // Type , Security Algo
        // Signature

        public string CreateToken(string userId, string email, string userName, IReadOnlyList<string> roles)
        {
            // Create Claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, userName)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            
            // Type
            var secKey = _jwtOptions.SecurityKey;
            if(string.IsNullOrWhiteSpace(secKey))
                throw new InvalidOperationException("Security key is required");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtOptions.ExpirationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class JwtSettings
    {
        public string SecurityKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpirationInMinutes { get; set; }
    }
}
