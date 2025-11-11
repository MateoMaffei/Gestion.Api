using Gestion.Api.Models.Entities;

namespace Gestion.Api.Models.Response
{
    public class CategoriaAtributoResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public Guid TipoDato { get; set; }
        public bool EsObligatorio { get; set; }

        public static explicit operator CategoriaAtributoResponse(CategoriaAtributo catAt)
        {
            return new CategoriaAtributoResponse
            {
                EsObligatorio = catAt.EsObligatorio,
                Id = catAt.IdGuid,
                Nombre = catAt.Nombre,
                TipoDato = catAt.TipoDato.IdGuid
            };
        }
    }
}
