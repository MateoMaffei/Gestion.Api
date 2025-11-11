using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Api.Models.Entities
{
    public class CategoriaAtributo
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdTipoDato { get; set; }
        public virtual TipoDato TipoDato { get; set; }
        public int IdCategoria { get; set; }
        public bool EsObligatorio { get; set; }

        public virtual Categoria Categoria { get; set; } = null!;
    }

    public class CategoriaAtributoMap : IEntityTypeConfiguration<CategoriaAtributo>
    {
        public void Configure(EntityTypeBuilder<CategoriaAtributo> builder)
        {
            builder.ToTable("CategoriaAtributo");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id);
            builder.Property(u => u.IdGuid).ValueGeneratedOnAdd().IsRequired().HasDefaultValueSql("(newid())");
            builder.Property(c => c.Nombre).IsRequired();
            builder.Property(c => c.IdTipoDato);
            builder.Property(c => c.IdCategoria).IsRequired();

            builder
            .HasOne(c => c.Categoria)
            .WithMany(c => c.Atributos)
            .HasForeignKey(c => c.IdCategoria)
            .HasConstraintName("FK_CategoriaAtributo_Categoria_IdCategoria");

            builder
            .HasOne(c => c.TipoDato)
            .WithMany()
            .HasForeignKey(c => c.IdTipoDato)
            .HasConstraintName("FK_CategoriaAtributo_TipoDato_IdTipoDato");
        }
    }

}
