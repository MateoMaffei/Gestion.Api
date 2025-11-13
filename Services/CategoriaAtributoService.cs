using Gestion.Api.Models.Entities;
using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;
using Gestion.Api.Repository.Interfaces;
using Gestion.Api.Services.Interfaces;

namespace Gestion.Api.Services
{
    public class CategoriaAtributoService : ICategoriaAtributoService
    {
        private readonly IGenericRepository<CategoriaAtributo> _categoriaAtributoRepository;
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        private readonly IGenericRepository<TipoDato> _tipoDatoRepository;
        public CategoriaAtributoService(IGenericRepository<CategoriaAtributo> categoriaAtributoRepository, 
                                        IGenericRepository<Categoria> categoriaRepository,
                                        IGenericRepository<TipoDato> tipoDatoRepository)
        {
            _categoriaAtributoRepository = categoriaAtributoRepository;
            _categoriaRepository = categoriaRepository;
            _tipoDatoRepository = tipoDatoRepository;
        }

        public async Task<IEnumerable<CategoriaAtributoResponse>> ObtenerCategoriasAtributoAsync(Guid idCategoria, Entidad entidad)
        {
            var categoria = await _categoriaRepository.GetByGuidAsync(idCategoria);

            if (categoria is null)
                throw new Exception($"No se encontro la categoria con id {idCategoria}");

            if (!categoria.IdEntidad.Equals(entidad.Id))
                throw new Exception("La categoria no corresponde a la entidad");

            var atributos = await _categoriaAtributoRepository.FindAsync(cat => cat.IdCategoria.Equals(categoria.Id));

            return atributos.Select(a => (CategoriaAtributoResponse)a);
        }

        public async Task<IEnumerable<CategoriaAtributoResponse>> CrearCategoriasAtributoAsync(Guid idCategoria, IEnumerable<CategoriaAtributoRequest> request)
        {
            var categoria = await _categoriaRepository.GetByGuidAsync(idCategoria);

            if (categoria is null)
                throw new Exception($"No se encontro la categoria con id {idCategoria}");

            var tiposDatos = await _tipoDatoRepository.GetAllAsync();
            
            if (tiposDatos is null)
                throw new Exception($"No se encontro el tipo de dato con id {idCategoria}");

            List<CategoriaAtributoResponse> response = new List<CategoriaAtributoResponse>();

            foreach (var item in request)
            {
                var tipoDato = tiposDatos.First(t => t.IdGuid.Equals(item.IdTipoDato));

                var atributo = new CategoriaAtributo
                {
                    EsObligatorio = item.EsObligatorio,
                    Nombre = item.Nombre,
                    IdTipoDato = tipoDato.Id,
                    IdCategoria = categoria.Id
                };

                response.Add((CategoriaAtributoResponse)await _categoriaAtributoRepository.AddAsync(atributo));
            }

            return response;
        }

        public async Task<CategoriaAtributoResponse> ActualizarCategoriasAtributoAsync(Guid idCategoria, Guid idCategoriaAtributo, CategoriaAtributoRequest request)
        {
            var categoria = await _categoriaRepository.GetByGuidAsync(idCategoria);

            if (categoria is null)
                throw new Exception($"No se encontro la categoria con id {idCategoria}");

            var atributo = await _categoriaAtributoRepository.GetByGuidAsync(idCategoriaAtributo);

            if (atributo is null)
                throw new Exception("No se encontro el atributo especificado.");

            var tipoDato = await _tipoDatoRepository.GetByGuidAsync(request.IdTipoDato);

            if (tipoDato is null)
                throw new Exception($"No se encontro el tipo de dato con id {idCategoria}");

            atributo.Nombre = request.Nombre;
            atributo.IdTipoDato = tipoDato.Id;
            atributo.EsObligatorio = request.EsObligatorio;

            await _categoriaAtributoRepository.UpdateAsync(atributo);

            return (CategoriaAtributoResponse)atributo;
        }

        public async Task EliminarCategoriasAtributoAsync(Guid idCategoria, Guid idCategoriaAtributo)
        {
            var categoria = await _categoriaRepository.GetByGuidAsync(idCategoria);

            if (categoria is null)
                throw new Exception($"No se encontro la categoria con id {idCategoria}");

            var atributo = await _categoriaAtributoRepository.FindAsync(atr => atr.IdCategoria.Equals(categoria.Id) 
                                                                            && atr.IdGuid.Equals(idCategoriaAtributo));

            if (atributo is null)
                throw new Exception("No se encontro el atributo especificado");

            await _categoriaAtributoRepository.DeleteAsync(atributo.First().Id);
        }
    }
}
