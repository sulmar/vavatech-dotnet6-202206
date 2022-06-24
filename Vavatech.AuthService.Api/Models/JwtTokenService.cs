using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Vavatech.AuthService.Api.Models
{
    public class JwtTokenService : ITokenService
    {
        public string Create(User user)
        {
            string secretKey = "your-256-bit-secret";
            string issuer = "Vavatech";

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),

                new Claim(ClaimTypes.Role, "trainer"),
                
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString())
            };

            // Install-Package System.IdentityModel.Tokens.Jwt

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,               
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials,
                claims: claims
                );

            var handler = new JwtSecurityTokenHandler();

            // Serializacja
            var token = handler.WriteToken(securityToken);

            return token;
        }
    }
}
