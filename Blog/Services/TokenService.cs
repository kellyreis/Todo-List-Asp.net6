using Blog.Models;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Blog.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
            var tokenDescriptot = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name, "kelly"),
                    new (ClaimTypes.Role, "admin"),
                    new ("Fruta","Banana")

                }),

                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(

                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)


            };
            var token = tokenHanlder.CreateToken(tokenDescriptot);


            return tokenHanlder.WriteToken(token);

        }
    }
}
