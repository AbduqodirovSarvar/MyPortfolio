using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                _configuration.ValidIssuer,
                _configuration.ValidAudience,
                claims.Concat(jwtClaim),
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
