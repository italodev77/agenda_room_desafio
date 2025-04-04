using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AgendaRoom.Config;
using AgendaRoom.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AgendaRoom.Service;

public class TokenService
{
    public static object GenerateToken(Usuarios usuario)
    {
        var key = Encoding.ASCII.GetBytes(Key.Secret);
        var TokenConfig = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
            {
                new Claim("idusuario", usuario.idUsuario.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(TokenConfig);
        var tokenString = tokenHandler.WriteToken(token);

        return new
        {
            token = tokenString,
        };

    }
}