using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestion.Api.Models.Entities;

namespace Gestion.Api.Models.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaAlta { get; set; }
        public int IdTipoUsuario { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
        public int IdEntidad { get; set; }
        public virtual Entidad Entidad { get; set; }
        public virtual RefreshToken? RefreshToken { get; set; }
    }

    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.IdGuid)
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasDefaultValueSql("(newid())");

            builder.Property(u => u.Username)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.Password)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(u => u.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.Apellido)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.FechaAlta)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(u => u.IdTipoUsuario)
                   .IsRequired();

            builder.Property(u => u.IdEntidad)
                   .IsRequired();

            // Relaciones
            builder.HasOne(u => u.TipoUsuario)
                   .WithMany()
                   .HasForeignKey(u => u.IdTipoUsuario)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.Entidad)
                   .WithMany()
                   .HasForeignKey(u => u.IdEntidad)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}


