namespace Gestion.Api.Models.Response
{
    public class TokenResponse
    {
        public string JwtToken { get; set; } = null!;
        public DateTime JwtTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiresAt { get; set; }
        public Guid RolId { get; set; }
        public string Rol { get; set; } = null!;
        public Guid IdEntidad { get; set; }
        public Guid UserIdGuid { get; set; }
        public string Username { get; set; } = null!;
    }
}
