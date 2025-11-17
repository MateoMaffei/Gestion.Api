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
    [Route("api/Categoria/{idCategoria:guid}/Atributos")]
    [Authorize]
    public class CategoriaAtributosController : ControllerBase
    {
        private readonly ILogger<CategoriaAtributosController> _logger;
        private readonly ICategoriaAtributoService _categoriaAtributoService;
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IGenericRepository<Entidad> _entidadRepository;

        public CategoriaAtributosController(ILogger<CategoriaAtributosController> logger,
                                            ICategoriaAtributoService categoriaAtributoService,
                                            IGenericRepository<Usuario> usuarioRepository,
                                            IGenericRepository<Categoria> categoriaRepository,
                                            IGenericRepository<Entidad> entidadRepository)

        { 
            _logger = logger;
            _categoriaAtributoService = categoriaAtributoService;
            _entidadRepository = entidadRepository;
            _usuarioRepository = usuarioRepository;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategoriasAtribustosAsync(Guid idCategoria)
        {
            var idEntidad = HttpContext.ObtenerIdEntidad();

            var entidad = await _entidadRepository.GetByGuidAsync(idEntidad);

            if (entidad is null)
                throw new Exception("No se encontro la entidad especificada.");

            var idUsuario = HttpContext.ObtenerIdUsuario();

            var usuario = await _usuarioRepository.GetByGuidAsync(idUsuario);

            if (usuario is null)
                throw new Exception("No se encontro el usuario especificado.");

            var response = await _categoriaAtributoService.ObtenerCategoriasAtributoAsync(idCategoria, entidad);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoriasAtribustosAsync(Guid idCategoria, [FromBody] List<CategoriaAtributoRequest> request)
        {
            var idEntidad = HttpContext.ObtenerIdEntidad();

            var entidad = await _entidadRepository.GetByGuidAsync(idEntidad);

            if (entidad is null)
                throw new Exception("No se encontro la entidad especificada.");

            var idUsuario = HttpContext.ObtenerIdUsuario();

            var usuario = await _usuarioRepository.GetByGuidAsync(idUsuario);

            if (usuario is null)
                throw new Exception("No se encontro el usuario especificado.");

            var response = await _categoriaAtributoService.CrearCategoriasAtributoAsync(idCategoria, request);

            return Ok(response);
        }

        [HttpPut("/api/Categoria/{idCategoria:guid}/Atributos/{idCategoriaAtributo:guid}")]
        public async Task<IActionResult> ActualizarCategoriasAtribustosAsync(Guid idCategoria, Guid idCategoriaAtributo, [FromBody] CategoriaAtributoRequest request)
        {
            var idEntidad = HttpContext.ObtenerIdEntidad();

            var entidad = await _entidadRepository.GetByGuidAsync(idEntidad);

            if (entidad is null)
                throw new Exception("No se encontro la entidad especificada.");

            var idUsuario = HttpContext.ObtenerIdUsuario();

            var usuario = await _usuarioRepository.GetByGuidAsync(idUsuario);

            if (usuario is null)
                throw new Exception("No se encontro el usuario especificado.");

            var response = _categoriaAtributoService.ActualizarCategoriasAtributoAsync(idCategoria, idCategoriaAtributo, request);

            return Ok(response);
        }

        [HttpDelete("/api/Categoria/{idCategoria:guid}/Atributos/{idCategoriaAtributo:guid}")]
        public async Task<IActionResult> EliminarCategoriasAtribustosAsync(Guid idCategoria, Guid idCategoriaAtributo)
        {
            var idEntidad = HttpContext.ObtenerIdEntidad();

            var entidad = await _entidadRepository.GetByGuidAsync(idEntidad);

            if (entidad is null)
                throw new Exception("No se encontro la entidad especificada.");

            var idUsuario = HttpContext.ObtenerIdUsuario();

            var usuario = await _usuarioRepository.GetByGuidAsync(idUsuario);

            if (usuario is null)
                throw new Exception("No se encontro el usuario especificado.");

            await _categoriaAtributoService.EliminarCategoriasAtributoAsync(idCategoria, idCategoriaAtributo);

            return Ok();
        }

    }
}
