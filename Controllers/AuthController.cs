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

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
            => Ok(await _auth.LoginAsync(request));
      

        [AllowAnonymous]
        [HttpPost("Refresh")]
        public async Task<ActionResult<TokenResponse>> Refresh([FromBody] RefreshRequest request)
            => Ok(await _auth.RefreshAsync(request));
    }
}
