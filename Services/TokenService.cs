using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Coodesh.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Coodesh;

public class TokenService
{
    public string GenerateToken(Usuario usuario)
    {
        var tokenHanlder = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "Coodesh"),
                new Claim(ClaimTypes.Role, "Admin")
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHanlder.CreateToken(tokenDescriptor);
        return tokenHanlder.WriteToken(token);
    }
}