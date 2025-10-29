using Gestion.Api.Services.Interfaces;
using Gestion.Api.Services;

namespace Gestion.Api.Configurations
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
