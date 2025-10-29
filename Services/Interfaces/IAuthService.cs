using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;

namespace Gestion.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> LoginAsync(LoginRequest request);
        Task<TokenResponse> RefreshAsync(RefreshRequest request);
    }
}
