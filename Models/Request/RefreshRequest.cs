namespace Gestion.Api.Models.Request
{
    public class RefreshRequest
    {
        public string RefreshToken  { get; set; }
        public int IdUsuario { get; set; }
    }
}
