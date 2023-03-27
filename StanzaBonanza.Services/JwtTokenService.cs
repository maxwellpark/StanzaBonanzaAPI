using Microsoft.IdentityModel.Tokens;
using StanzaBonanza.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StanzaBonanza.Services
{
    public class JwtTokenService : ITokenService
    {
        public string GenerateToken(IEnumerable<Claim> claims, string key, string issuer)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
