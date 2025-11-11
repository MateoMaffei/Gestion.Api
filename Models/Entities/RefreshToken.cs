using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Api.Models.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public int IdUsuario { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; }
        public bool Revocado { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
    }

    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.IdGuid).ValueGeneratedOnAdd().IsRequired().HasDefaultValueSql("(newid())");
            builder.Property(e => e.IdUsuario).IsRequired();
            builder.Property(e => e.Token).HasMaxLength(500).IsRequired();
            builder.Property(e => e.Expiracion).IsRequired();
            builder.Property(e => e.Revocado).HasDefaultValue(false);

            builder.HasOne(e => e.Usuario)
                   .WithOne(u => u.RefreshToken)
                   .HasForeignKey<RefreshToken>(e => e.IdUsuario)
                   .HasPrincipalKey<Usuario>(u => u.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
