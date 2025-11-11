using Gestion.Api.Configurations.Options;
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
                new Claim(ClaimTypes.Name, u.Username),
                new Claim("entidad", u.IdEntidad.ToString()),
                new Claim("rolId", u.IdTipoUsuario.ToString()),
                new Claim("rol", u.TipoUsuario?.Descripcion ?? u.IdTipoUsuario.ToString())
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
