using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Gestion.Api.Configurations.Options;
using Gestion.Api.Helpers;
using Gestion.Api.Models.Entities;
using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;
using Gestion.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ApplicationDbContext = Gestion.Api.Repository.ApplicationDbContext;

namespace Gestion.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly JwtSettingsOptions _jwtSettingsOptions;

        public AuthService(ApplicationDbContext db, IOptions<JwtSettingsOptions> cfg)
        {
            _db = db;
            _jwtSettingsOptions = cfg.Value;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            var user = await _db.Usuarios
                .Include(u => u.TipoUsuario)
                .Include(u => u.Entidad)
                .FirstOrDefaultAsync(u =>
                    u.Username.ToLower() == request.Username.ToLower() &&
                    u.Entidad.IdGuid == request.IdEntidad);

            if (user is null)
                throw new InvalidOperationException("Usuario o contraseña inválidos.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new InvalidOperationException("Credenciales inválidas.");

            // 🔐 Desencriptar password recibido
            var decryptedPass = EncryptionHelper.DecryptStringAES(request.Password, _jwtSettingsOptions.Key);

            // Comparar contra hash almacenado
            var valid = BCrypt.Net.BCrypt.Verify(decryptedPass, user.Password);
            if (!valid)
                throw new InvalidOperationException("Credenciales inválidas.");

            var (access, aexp) = CreateAccessToken(user);
            var (refresh, rexp) = CreateRefreshToken();

            // Si quieres persistir refresh token por usuario:
            // (no cambia tu modelo; podés crear una tabla/columna luego)
            // Por ahora lo devolvemos sin persistir.

            return new TokenResponse
            {
                AccessToken = access,
                AccessTokenExpiresAt = aexp,
                RefreshToken = refresh,
                RefreshTokenExpiresAt = rexp,
                RolId = user.IdTipoUsuario,
                Rol = user.TipoUsuario?.Descripcion ?? user.IdTipoUsuario.ToString(),
                IdEntidad = user.IdEntidad,
                UserIdGuid = user.IdGuid,
                Username = user.Username
            };
        }

        public async Task<TokenResponse> RefreshAsync(RefreshRequest request)
        {
            // Aquí validarías contra persistencia si decides guardarlos.
            // Por ahora rechazamos refresh inválidos por formato.
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                throw new InvalidOperationException("Refresh token inválido.");

            // Estrategia mínima: exigir un access token vencido y reemitir
            // si el cliente presenta un refresh. En esta versión demo,
            // pedimos que el cliente envíe también Username si quisieras reforzar.

            throw new NotImplementedException("Persistencia de refresh tokens pendiente.");
        }

        private (string token, DateTime exp) CreateAccessToken(Usuario u)
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

            return (new JwtSecurityTokenHandler().WriteToken(token), exp);
        }

        private (string token, DateTime exp) CreateRefreshToken()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Fill(bytes);
            var token = Convert.ToBase64String(bytes);
            var exp = DateTime.UtcNow.AddDays(_jwtSettingsOptions.RefreshTokenDays);
            return (token, exp);
        }
    }
}
