using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Adocao.Models;
using Microsoft.IdentityModel.Tokens;

namespace Adocao.Services;

public class TokenService
{
    public object GerarToken(Administrador administrador)
    {
        var key = Encoding.ASCII.GetBytes(Configuration.Secret);
        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("administradorId", administrador.Id.ToString()),
                new Claim("nomeAdministrador", administrador.Nome)
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        var tokenString = tokenHandler.WriteToken(token);

        return new
        {
            token = tokenString,
            nome = administrador.Nome,
            id = administrador.Id
        };
    }

}