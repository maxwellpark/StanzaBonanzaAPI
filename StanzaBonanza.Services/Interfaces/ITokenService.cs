using System.Security.Claims;

namespace StanzaBonanza.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims, string key, string issuer);
    }
}
