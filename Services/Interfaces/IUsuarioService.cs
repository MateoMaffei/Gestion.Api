using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;

namespace Gestion.Api.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> CrearUsuarioAsync(UsuarioCreateRequest request);
    }
}
