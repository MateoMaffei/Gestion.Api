using Gestion.Api.Models.Entities;
using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;

namespace Gestion.Api.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaResponse> CrearCategoria(CrearCategoriaRequest request, Entidad entidad);
        Task<List<CategoriaResponse>?> ObtenerCategorias(Entidad entidad);
        Task<CategoriaResponse> ModificarCategoria(Guid idCategoria, ModificarCategoriaRequest request);
        Task EliminarCategoria(Guid idCategoria);
    }
}
