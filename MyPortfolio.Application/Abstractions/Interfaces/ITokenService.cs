using System.Security.Claims;

namespace MyPortfolio.Application.Abstractions.Interfaces
{
    public interface ITokenService
    {
        string GetAccessToken(Claim[] claims);
    }
}
