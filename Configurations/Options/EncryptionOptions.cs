namespace Gestion.Api.Configurations.Options
{
    public class EncryptionOptions
    {
        public string AesKey { get; set; }
    }

    public static class ConfigEncryptionOptions
    {
        public static string Route = "Encryption";

        public static EncryptionOptions AddEncryptionOptions(this IConfiguration config, WebApplicationBuilder builder)
        {
            var encryptionOptions = config.GetSection(Route);
            builder.Services.Configure<EncryptionOptions>(encryptionOptions);

            var configEncryptionOptions = encryptionOptions.Get<EncryptionOptions>()!;
            return configEncryptionOptions;
        }
    }
}
