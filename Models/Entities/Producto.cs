using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Api.Models.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<ProductoAtributoValor> Propiedades { get; set; }
    }

    public class ProductoMap : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Producto");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id);
            builder.Property(u => u.IdGuid).ValueGeneratedOnAdd().IsRequired().HasDefaultValueSql("(newid())");
            builder.Property(c => c.Nombre).IsRequired();
            builder.Property(c => c.Descripcion);
            builder.Property(c => c.IdCategoria).IsRequired();

            builder.HasOne(c => c.Categoria)
                   .WithMany()
                   .HasForeignKey(c => c.IdCategoria)
                   .HasConstraintName("FK_Producto_Categoria_IdCategoria");
        }
    }
}
