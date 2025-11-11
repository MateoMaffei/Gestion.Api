using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestion.Api.Models.Entities.Estructuras;
using static Gestion.Api.Helpers.Enums;
namespace Gestion.Api.Models.Entities
{

    public class TipoUsuario
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Descripcion { get; set; }


        /* Propiedades estaticas */
        public static readonly StructTipoUsuario Admin = new StructTipoUsuario
            ((int)EnumTipoUsuario.Aministrador, new Guid("31C76205-6DDD-43A6-8BC4-0C748DDFCFFA"), "Aministrador");

        public static readonly StructTipoUsuario Empleado = new StructTipoUsuario
            ((int)EnumTipoUsuario.Empleado, new Guid("C1A321CC-7A0A-4079-A6D2-D17F9F045FD3"), "Empleado");

        public static readonly StructTipoUsuario Usuario = new StructTipoUsuario
            ((int)EnumTipoUsuario.Usuario, new Guid("AD9B7732-2A84-4C78-9A47-717D5B47EEF0"), "Usuario");
    }

    public class TipoUsuarioMap : IEntityTypeConfiguration<TipoUsuario>
    {
        public void Configure(EntityTypeBuilder<TipoUsuario> builder)
        {
            builder.ToTable("TipoUsuario");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.IdGuid).ValueGeneratedOnAdd().IsRequired().HasDefaultValueSql("(newid())");
            builder.Property(t => t.Descripcion).HasMaxLength(150).IsRequired();

            builder.HasData(new TipoUsuario
            {
                Id = TipoUsuario.Admin.Id,
                IdGuid = TipoUsuario.Admin.IdGuid,
                Descripcion = TipoUsuario.Admin.Descripcion,
            }, 
            new TipoUsuario{
                Id = TipoUsuario.Empleado.Id,
                IdGuid = TipoUsuario.Empleado.IdGuid,
                Descripcion = TipoUsuario.Empleado.Descripcion,
            }, 
            new TipoUsuario{
                Id = TipoUsuario.Usuario.Id,
                IdGuid = TipoUsuario.Usuario.IdGuid,
                Descripcion = TipoUsuario.Usuario.Descripcion,
            });
        }
    }
}

