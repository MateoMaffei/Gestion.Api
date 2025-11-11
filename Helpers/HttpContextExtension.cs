using System.IdentityModel.Tokens.Jwt;

namespace Gestion.Api.Helpers
{
    public static class HttpContextExtension
    {

        public static Guid ObtenerIdEntidad(this HttpContext context)
        {
            var id = context.User.Claims.FirstOrDefault(x => x.Type.Equals(Constantes.Claim_Entidad));

            if (id is null)
                throw new ArgumentException("Credenciales invalidas");

            return new Guid(id.Value.ToString());
        }

        public static Guid ObtenerIdUsuario(this HttpContext context)
        {
            var id = context.User.Claims.FirstOrDefault(x => x.Type.Equals(Constantes.Claim_Usuario));

            if (id is null)
                throw new ArgumentException("Credenciales invalidas");

            return new Guid(id.Value.ToString());
        }

    }
}
