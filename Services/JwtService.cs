using Gestion.Api.Configurations.Options;
using Gestion.Api.Helpers;
using Gestion.Api.Models.Entities;
using Gestion.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Gestion.Api.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettingsOptions _jwtSettingsOptions;
        public JwtService(IOptions<JwtSettingsOptions> jwtSettingsOptions)
        {
            _jwtSettingsOptions = jwtSettingsOptions.Value;
        }
        public (string token, DateTime exp) CreateAccessToken(Usuario u)
        {
            var exp = DateTime.UtcNow.AddMinutes(_jwtSettingsOptions.AccessTokenMinutes);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingsOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, u.IdGuid.ToString()),
                new Claim(Constantes.Claim_Username, u.Username),
                new Claim(Constantes.Claim_Entidad, u.Entidad.IdGuid.ToString()),
                new Claim(Constantes.Claim_Usuario, u.IdGuid.ToString()),
                new Claim(Constantes.Claim_IdRol, u.TipoUsuario.IdGuid.ToString()),
                new Claim(Constantes.Claim_Rol, u.TipoUsuario?.Descripcion ?? u.IdTipoUsuario.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettingsOptions.Issuer,
                audience: _jwtSettingsOptions.Audience,
                claims: claims,
                expires: exp,
                signingCredentials: creds);

            var bearerToken = $"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}";
            return ( bearerToken, exp);
        }

        public (string token, DateTime exp) CreateRefreshToken()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Fill(bytes);
            var token = Convert.ToBase64String(bytes);
            var exp = DateTime.UtcNow.AddDays(_jwtSettingsOptions.RefreshTokenDays);
            return (token, exp);
        }
    }
}
