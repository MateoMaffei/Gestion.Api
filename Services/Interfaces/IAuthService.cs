using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;

namespace Gestion.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> LoginAsync(LoginRequest request);
        Task<TokenResponse> RefreshTokenAsync(string token);
        Task RegistrarseAsync(NuevoUsuarioRequest request);
    }
}
