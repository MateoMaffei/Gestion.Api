using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestion.Api.Models.Entities;

namespace Gestion.Api.Models.Entities
{
    public class Entidad
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Nombre { get; set; }
    }

    public class EntidadMap : IEntityTypeConfiguration<Entidad>
    {
        public void Configure(EntityTypeBuilder<Entidad> builder)
        {
            builder.ToTable("Entidad");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.IdGuid)
                   .IsRequired();

            builder.Property(e => e.Nombre)
                   .HasMaxLength(200)
                   .IsRequired();
        }
    }
}