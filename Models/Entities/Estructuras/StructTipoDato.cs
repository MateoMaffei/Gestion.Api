namespace Gestion.Api.Models.Entities.Estructuras
{
    public struct StructTipoDato
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Tipo { get; set; }

        public StructTipoDato(int id, Guid idGuid, string tipo)
        {
            Id = id;
            IdGuid = idGuid;
            Tipo = tipo;
        }
    }
}
