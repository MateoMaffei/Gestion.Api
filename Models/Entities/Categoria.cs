using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Api.Models.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Descripcion { get; set; }
        public string? Icono { get; set; }
        public int IdEntidad { get; set; }
        public virtual Entidad Entidad { get; set; }
        public virtual ICollection<CategoriaAtributo> Atributos { get; set; }
        
    }

    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id);
            builder.Property(u => u.IdGuid).ValueGeneratedOnAdd().IsRequired().HasDefaultValueSql("(newid())");
            builder.Property(c => c.Descripcion).IsRequired();
            builder.Property(c => c.Icono);
            builder.Property(c => c.IdEntidad).IsRequired();

            builder
            .HasOne(c => c.Entidad)
            .WithMany()
            .HasForeignKey(c => c.IdEntidad)
            .HasConstraintName("FK_Categoria_Entidad_IdEntidad");
        }
    }
}
