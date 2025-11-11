using Gestion.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gestion.Api.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TiposUsuario { get; set; }
        public DbSet<Entidad> Entidades { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<CategoriaAtributo> CategoriaAtributo { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<ProductoAtributoValor> ProductoAtributoValor { get; set; }
        public DbSet<TipoDato> TipoDato { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
