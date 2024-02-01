using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyPortfolio.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTConfiguration _configuration;

        public TokenService(IOptions<JWTConfiguration> config)
        {
            _configuration = config.Value;
        }

        public string GetAccessToken(Claim[] claims)
        {
            Claim[] jwtClaim = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, DateTime.UtcNow.ToString()),
            };

            var jwtClaims = claims.Concat(jwtClaim);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));

            if (key.KeySize < 256)
            {
                // Ensure key size is at least 256 bits
                var largerKeyBytes = new byte[32];
                var rng = RandomNumberGenerator.Create();
                rng.GetBytes(largerKeyBytes);
                key = new SymmetricSecurityKey(largerKeyBytes);
            }

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration.ValidIssuer,
                _configuration.ValidAudience,
                jwtClaims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
