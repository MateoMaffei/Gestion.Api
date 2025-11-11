using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Api.Models.Entities
{
    public class ProductoAtributoValor
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public int IdProducto { get; set; }
        public int IdCategoriaAtributo { get; set; }
        public string Valor { get; set; } = null!;

        public virtual Producto Producto { get; set; } = null!;
        public virtual CategoriaAtributo CategoriaAtributo { get; set; } = null!;
    }

    public class ProductoAtributoValorMap : IEntityTypeConfiguration<ProductoAtributoValor>
    {
        public void Configure(EntityTypeBuilder<ProductoAtributoValor> builder)
        {
            builder.ToTable("ProductoAtributoValor");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id);
            builder.Property(u => u.IdGuid).ValueGeneratedOnAdd().IsRequired().HasDefaultValueSql("(newid())");            
            builder.Property(c => c.Valor);
            builder.Property(c => c.IdProducto).IsRequired();
            builder.Property(c => c.IdCategoriaAtributo).IsRequired();

            builder
            .HasOne(c => c.Producto)
            .WithMany(c => c.Propiedades)
            .HasForeignKey(c => c.IdProducto)
            .OnDelete(DeleteBehavior.Cascade);

            builder
            .HasOne(c => c.CategoriaAtributo)
            .WithMany()
            .HasForeignKey(c => c.IdCategoriaAtributo)
            .HasConstraintName("FK_ProductoAtributoValor_CategoriaAtributo_IdCategoriaAtributo")
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
