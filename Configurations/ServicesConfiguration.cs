using Gestion.Api.Services.Interfaces;
using Gestion.Api.Services;
using Gestion.Api.Repository.Interfaces;
using Gestion.Api.Repository;

namespace Gestion.Api.Configurations
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ICategoriaAtributoService, CategoriaAtributoService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
