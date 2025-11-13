namespace Gestion.Api.Models.Request
{
    public class ModificarCategoriaRequest
    {
        public string Descripcion { get; set; } = null!;
        public string? Icono { get; set; }
    }
}
