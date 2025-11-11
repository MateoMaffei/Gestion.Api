namespace Gestion.Api.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task GuardarTokenAsync(string token, DateTime exp, int idUsuario);
    }
}
