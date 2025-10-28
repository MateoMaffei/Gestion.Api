namespace Gestion.Api.Models.Request
{
    public class UsuarioCreateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdTipoUsuario { get; set; }
        public int IdEntidad { get; set; }
    }
}
