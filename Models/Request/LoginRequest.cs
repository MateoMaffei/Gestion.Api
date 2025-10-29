namespace Gestion.Api.Models.Request
{
    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid IdEntidad { get; set; }
    }
}
