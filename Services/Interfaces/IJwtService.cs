using Gestion.Api.Models.Entities;

namespace Gestion.Api.Services.Interfaces
{
    public interface IJwtService
    {
        (string token, DateTime exp) CreateAccessToken(Usuario u);
        (string token, DateTime exp) CreateRefreshToken();
    }
}
