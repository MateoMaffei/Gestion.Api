using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Api.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CrearUsuarioAsync()
        {

            return Ok("Usuario Creado");
        }
    }
}
