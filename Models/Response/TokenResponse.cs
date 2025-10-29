namespace Gestion.Api.Models.Response
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = null!;
        public DateTime AccessTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiresAt { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; } = null!;
        public int IdEntidad { get; set; }
        public Guid UserIdGuid { get; set; }
        public string Username { get; set; } = null!;
    }
}
