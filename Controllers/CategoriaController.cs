using Gestion.Api.Helpers;
using Gestion.Api.Models.Entities;
using Gestion.Api.Models.Request;
using Gestion.Api.Repository.Interfaces;
using Gestion.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Api.Controllers
{
    [ApiController]
    [Route("api/Categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IGenericRepository<Entidad> _entidadRepository;
        private readonly ICategoriaService _categoriaService; 

        public CategoriaController(ILogger<CategoriaController> logger, 
                                   IGenericRepository<Usuario> usuarioRepository, 
                                   IGenericRepository<Entidad> entidadRepository,
                                   ICategoriaService categoriaService)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _entidadRepository = entidadRepository;
            _categoriaService = categoriaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoriaAsync([FromBody] CrearCategoriaRequest request)
        {
            var idEntidad = HttpContext.ObtenerIdEntidad();

            var entidad = await _entidadRepository.GetByGuidAsync(idEntidad);

            if (entidad is null)
                throw new Exception("No se encontro la entidad especificada.");

            var idUsuario = HttpContext.ObtenerIdUsuario();

            var usuario = await _usuarioRepository.GetByGuidAsync(idUsuario);

            if (usuario is null)
                throw new Exception("No se encontro el usuario especificado.");

            var response = await _categoriaService.CrearCategoria(request, entidad);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasAsync()
        {
            var idEntidad = HttpContext.ObtenerIdEntidad();

            var entidad = await _entidadRepository.GetByGuidAsync(idEntidad);

            if (entidad is null)
                throw new Exception("No se encontro la entidad especificada.");

            var idUsuario = HttpContext.ObtenerIdUsuario();

            var usuario = await _usuarioRepository.GetByGuidAsync(idUsuario);

            if (usuario is null)
                throw new Exception("No se encontro el usuario especificado.");

            var categorias = await _categoriaService.ObtenerCategorias(entidad);

            return Ok(categorias);
        }

        [HttpPut("/api/Categoria/{idCategoria:guid}")]
        public async Task<IActionResult> ModificarCategoriaAsync(Guid idCategoria, [FromBody] ModificarCategoriaRequest request)
        {
            var idEntidad = HttpContext.ObtenerIdEntidad();

            var entidad = await _entidadRepository.GetByGuidAsync(idEntidad);

            if (entidad is null)
                throw new Exception("No se encontro la entidad especificada.");

            var idUsuario = HttpContext.ObtenerIdUsuario();

            var usuario = await _usuarioRepository.GetByGuidAsync(idUsuario);

            if (usuario is null)
                throw new Exception("No se encontro el usuario especificado.");

            var response = await _categoriaService.ModificarCategoria(idCategoria, request);

            return Ok(response);
        }

        [HttpDelete("/api/Categoria/{idCategoria:guid}")]
        public async Task<IActionResult> EliminarCategoriaAsync(Guid idCategoria)
        {
            var idEntidad = HttpContext.ObtenerIdEntidad();

            var entidad = await _entidadRepository.GetByGuidAsync(idEntidad);

            if (entidad is null)
                throw new Exception("No se encontro la entidad especificada.");

            var idUsuario = HttpContext.ObtenerIdUsuario();

            var usuario = await _usuarioRepository.GetByGuidAsync(idUsuario);

            if (usuario is null)
                throw new Exception("No se encontro el usuario especificado.");

            await _categoriaService.EliminarCategoria(idCategoria);

            return Ok();
        }

    }
}
