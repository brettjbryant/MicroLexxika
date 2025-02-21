using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using MicroLexxika.Domain.Models;

namespace MicroLexxika.Domain.Helpers
{
    public class TokenHelper
    {
        public Token CreateToken(string username, string role, string secret, int expiryMin)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                ]),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(expiryMin),
            };
            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return new Token { Value = token, Expires = expiryMin.ToString() };
        }
    }
}
