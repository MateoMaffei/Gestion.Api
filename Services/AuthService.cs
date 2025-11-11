using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Gestion.Api.Configurations.Options;
using Gestion.Api.Helpers;
using Gestion.Api.Models.Entities;
using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;
using Gestion.Api.Repository;
using Gestion.Api.Repository.Interfaces;
using Gestion.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gestion.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly JwtSettingsOptions _jwtSettingsOptions;
        private readonly EncryptionOptions _encryptionOptions;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IJwtService _jwtService;

        public AuthService(ApplicationDbContext db, 
                           ILogger<AuthService> logger, 
                           ApplicationDbContext context, 
                           IOptions<JwtSettingsOptions> jwtSettingsOptions,
                           IOptions<EncryptionOptions> encryptionOptions,
                           IRefreshTokenService refreshTokenService,
                           IJwtService jwtService)
        {
            _context = db;
            _logger = logger;
            _context = context;
            _jwtSettingsOptions = jwtSettingsOptions.Value;
            _encryptionOptions = encryptionOptions.Value;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .Include(u => u.Entidad)
                .FirstOrDefaultAsync(u =>
                    u.Username.ToLower() == request.Username.ToLower() &&
                    u.Entidad.IdGuid == request.IdEntidad);

            if (user is null)
                throw new InvalidOperationException("Usuario o contraseña inválidos.");

            var decryptedPassword = EncryptionHelper.DecryptAES(request.Password, _encryptionOptions.AesKey);

            if (!BCrypt.Net.BCrypt.Verify(decryptedPassword, user.PasswordHash))
                throw new InvalidOperationException("Contraseña incorrecta");

            var (token, tokenExpiration) = _jwtService.CreateAccessToken(user);
            var (refreshToken, refreshTokenExpiration) = _jwtService.CreateRefreshToken();

            await _refreshTokenService.GuardarTokenAsync(refreshToken, refreshTokenExpiration, user.Id);

            await _context.SaveChangesAsync();

            return new TokenResponse
            {
                JwtToken = token,
                JwtTokenExpiresAt = tokenExpiration,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = refreshTokenExpiration,
                RolId = user.TipoUsuario.IdGuid,
                Rol = user.TipoUsuario?.Descripcion ?? user.IdTipoUsuario.ToString(),
                IdEntidad = user.Entidad.IdGuid,
                UserIdGuid = user.IdGuid,
                Username = user.Username
            };
        }

        public async Task<TokenResponse> RefreshTokenAsync(string token)
        {
            var usuario = await _context.Usuarios
                                        .Include(u => u.RefreshToken)
                                        .Include(u => u.TipoUsuario)
                                        .Include(u => u.Entidad)
                                        .FirstOrDefaultAsync(x => x.RefreshToken != null && x.RefreshToken.Token.Equals(token));

            if(usuario is null)
                throw new InvalidOperationException("Se produjo un error o los datos enviados no corresponden.");

            var (newToken, tokenExpiration) = _jwtService.CreateAccessToken(usuario);
            var (refreshToken, refreshTokenExpiration) = _jwtService.CreateRefreshToken();

            await _refreshTokenService.GuardarTokenAsync(refreshToken, refreshTokenExpiration, usuario.Id);

            await _context.SaveChangesAsync();

            return new TokenResponse
            {
                JwtToken = newToken,
                JwtTokenExpiresAt = tokenExpiration,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = refreshTokenExpiration,
                RolId = usuario.TipoUsuario.IdGuid,
                Rol = usuario.TipoUsuario?.Descripcion ?? usuario.IdTipoUsuario.ToString(),
                IdEntidad = usuario.Entidad.IdGuid,
                UserIdGuid = usuario.IdGuid,
                Username = usuario.Username
            };
        }

        public async Task RegistrarseAsync(NuevoUsuarioRequest request)
        {
            var entidad = await _context.Entidades.FirstOrDefaultAsync(e => e.IdGuid.Equals(request.IdEntidad));

            if (entidad == null)
                throw new InvalidOperationException("Se produjo un error o los datos enviados no corresponden.");

            var tipoUsuario = await _context.TiposUsuario.FirstOrDefaultAsync(e => e.IdGuid.Equals(request.IdTipoUsuario));

            if (tipoUsuario == null)
                throw new InvalidOperationException("Se produjo un error o los datos enviados no corresponden.");

            var existeUsername = await _context.Usuarios.AnyAsync(u =>  u.Username.Equals(request.Username));

            if (existeUsername)
                throw new InvalidOperationException("El username ya existe, intente iniciar sesión.");

            var decrypted = EncryptionHelper.DecryptAES(request.Password, _encryptionOptions.AesKey);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(decrypted);

            await _context.Usuarios.AddAsync(new Usuario
            {
                Nombre = request.Nombre,
                IdGuid = Guid.NewGuid(),
                Apellido = request.Apellido,
                IdEntidad = entidad.Id,
                FechaAlta = DateTime.Now,
                Password = request.Password,
                PasswordHash = passwordHash,
                IdTipoUsuario = tipoUsuario.Id,
                Username = request.Username,
            });
            
            await _context.SaveChangesAsync();
        }
    }
}
