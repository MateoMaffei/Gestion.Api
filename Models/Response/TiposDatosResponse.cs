using Gestion.Api.Models.Entities;

namespace Gestion.Api.Models.Response
{
    public class TiposDatosResponse
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }

        public static explicit operator TiposDatosResponse(TipoDato t)
        {
            return new TiposDatosResponse
            {
                Id = t.IdGuid,
                Tipo = t.Tipo,
            };
        }
    }
}
