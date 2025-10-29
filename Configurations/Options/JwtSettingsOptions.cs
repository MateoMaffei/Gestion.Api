namespace Gestion.Api.Configurations.Options
{
    public class JwtSettingsOptions
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!;
        public int AccessTokenMinutes { get; set; } = 30;
        public int RefreshTokenDays { get; set; } = 7;
    }

    public static class ConfigJwtSettingsOptions
    {
        public static string Route = "Jwt";

        public static JwtSettingsOptions AddJwtOptions(this IConfiguration config, WebApplicationBuilder builder)
        {
            var jwtSettingsOptions = config.GetSection(Route);
            builder.Services.Configure<JwtSettingsOptions>(jwtSettingsOptions);
            var configJwtSettingsOptions = jwtSettingsOptions.Get<JwtSettingsOptions>()!;
            return configJwtSettingsOptions;
        }
    }
}
