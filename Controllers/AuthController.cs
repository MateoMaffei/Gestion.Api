using Azure.Core;
using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;
using Gestion.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
        {
            return Ok(await _auth.LoginAsync(request));
        }

        [HttpPost]
        [HttpPost("Refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponse>> RefreshTockenAsync([FromBody] RefreshTokenRequest request )
        {
            return Ok(await _auth.RefreshTokenAsync(request.Token));
        }

        [AllowAnonymous]
        [HttpPost("Registro")]
        public async Task Login([FromBody] NuevoUsuarioRequest request)
        {
            await _auth.RegistrarseAsync(request);
        }
    }
}   
