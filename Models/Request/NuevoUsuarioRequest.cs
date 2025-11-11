namespace Gestion.Api.Models.Request
{
    public class NuevoUsuarioRequest
    {
        public Guid IdTipoUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid IdEntidad { get; set; }
    }
}
