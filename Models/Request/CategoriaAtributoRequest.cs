namespace Gestion.Api.Models.Request
{
    public class CategoriaAtributoRequest
    {
        public string Nombre { get; set; } = null!;
        public Guid IdTipoDato { get; set; }
        public bool EsObligatorio { get; set; }
    }
}
