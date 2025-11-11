using Gestion.Api.Models.Entities;

namespace Gestion.Api.Models.Response
{
    public class CategoriaResponse
    {
        public Guid IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public string? Icono { get; set; }

        public static explicit operator CategoriaResponse(Categoria cat)
        {
            return new CategoriaResponse
            {
                Descripcion = cat.Descripcion,
                Icono = cat.Icono,
                IdCategoria = cat.IdGuid
            };
        }
    }
}
