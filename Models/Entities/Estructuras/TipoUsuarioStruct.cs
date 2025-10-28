namespace Gestion.Api.Models.Entities.Estructuras
{    
    public struct StructTipoUsuario
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Descripcion { get; set; }

        public StructTipoUsuario(int id, Guid idGuid, string descripcion)
        {
            Id = id;
            IdGuid = idGuid;
            Descripcion = descripcion;
        }
    }
    
}
