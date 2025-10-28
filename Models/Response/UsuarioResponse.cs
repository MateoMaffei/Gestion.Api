namespace Gestion.Api.Models.Response
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoUsuario { get; set; }
        public string Entidad { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
