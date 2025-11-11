using Gestion.Api.Models.Entities;
using Gestion.Api.Repository.Interfaces;
using Gestion.Api.Services.Interfaces;

namespace Gestion.Api.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public readonly IGenericRepository<RefreshToken> _tokenRepository;
        public RefreshTokenService(IGenericRepository<RefreshToken> tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task GuardarTokenAsync(string token, DateTime exp, int idUsuario)
        {
            var tokenAnterior = (await _tokenRepository.FindAsync(t => t.IdUsuario == idUsuario)).FirstOrDefault();


            if (tokenAnterior == null)
            {
                await _tokenRepository.AddAsync(new RefreshToken
                {
                    IdGuid = Guid.NewGuid(),
                    IdUsuario = idUsuario,
                    Expiracion = exp,
                    Token = token,
                });
            }
            else
            {
                tokenAnterior.Expiracion = exp;
                tokenAnterior.Token = token;
                await _tokenRepository.UpdateAsync(tokenAnterior);
            }
        }
    }
}
