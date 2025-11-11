using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        [HttpPost]        
        public async Task CrearCategoria()
        {
            var user = HttpContext.User;

        }
    }
}
