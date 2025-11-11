using Azure.Core;
using Gestion.Api.Models.Entities;
using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;
using Gestion.Api.Repository.Interfaces;
using Gestion.Api.Services.Interfaces;

namespace Gestion.Api.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ILogger<CategoriaService> _logger;
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        public CategoriaService(IGenericRepository<Categoria> categoriaRepository,
                                ILogger<CategoriaService> logger)
        {
            _categoriaRepository = categoriaRepository;
            _logger = logger;
        }

        public async Task<CategoriaResponse> CrearCategoria(CrearCategoriaRequest request, Entidad entidad)
        {
            var existeCategoriaParaEntidad = await _categoriaRepository.ExistsAsync(cat => cat.Descripcion.ToLower().Equals(request.Descripcion.ToLower())
                                                                                        && cat.IdEntidad.Equals(entidad.Id));

            if (existeCategoriaParaEntidad)
                throw new Exception("Categoria existente.");

            var nuevaCategoria = new Categoria()
            {
                Descripcion = request.Descripcion,
                IdEntidad = entidad.Id,
                Icono = request.Icono,
                IdGuid = new Guid()
            };

            var categoria = await _categoriaRepository.AddAsync(nuevaCategoria);

            return (CategoriaResponse)categoria;
        }

        public async Task EliminarCategoria(Guid idCategoria)
        {
            var categoria = await _categoriaRepository.GetByGuidAsync(idCategoria);

            if (categoria is null)
                throw new Exception("Datos proporcionados incorrectos.");

            await _categoriaRepository.DeleteAsync(categoria.Id);
        }

        public async Task<CategoriaResponse> ModificarCategoria(Guid idCategoria, ModificarCategoriaRequest request)
        {
            var categoria = await _categoriaRepository.GetByGuidAsync(idCategoria);

            if (categoria is null)
                throw new Exception("Datos proporcionados incorrectos.");

            categoria.Descripcion = request.Descripcion;
            categoria.Icono = request.Icono;

            await _categoriaRepository.UpdateAsync(categoria);

            return (CategoriaResponse)categoria;
        }

        public async Task<List<CategoriaResponse>?> ObtenerCategorias(Entidad entidad)
        {
            var categorias = await _categoriaRepository.FindAsync(cat => cat.IdEntidad.Equals(entidad.Id));

            return categorias.Select(cat => (CategoriaResponse)cat).ToList();
        }
    }
}
