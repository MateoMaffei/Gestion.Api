using Gestion.Api.Models.Entities;
using Gestion.Api.Models.Response;
using Gestion.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/TipoDato")]
    public class TipoDatoController : ControllerBase
    {
        private readonly ILogger<TipoDatoController> _logger;
        private readonly IGenericRepository<TipoDato> _tipoDatoRepository;
        public TipoDatoController(ILogger<TipoDatoController> logger,
                                  IGenericRepository<TipoDato> tipoDatoRepository)
        {
            _logger = logger;
            _tipoDatoRepository = tipoDatoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTiposDatosAsync()
        {
            var tiposDatos = await _tipoDatoRepository.GetAllAsync();
            return Ok(tiposDatos.Select(t => (TiposDatosResponse)t));
        }
    }
}
