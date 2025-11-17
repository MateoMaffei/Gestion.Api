using Gestion.Api.Models.Entities;
using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;

namespace Gestion.Api.Services.Interfaces
{
    public interface ICategoriaAtributoService
    {
        Task<IEnumerable<CategoriaAtributoResponse>> ObtenerCategoriasAtributoAsync(Guid idCategoria, Entidad entidad);
        Task<IEnumerable<CategoriaAtributoResponse>> CrearCategoriasAtributoAsync(Guid idCategoria, IEnumerable<CategoriaAtributoRequest> request);
        Task<CategoriaAtributoResponse> ActualizarCategoriasAtributoAsync(Guid idCategoria, Guid idCategoriaAtributo, CategoriaAtributoRequest request);
        Task EliminarCategoriasAtributoAsync(Guid idCategoria, Guid idCategoriaAtributo);
    }
}
