using Gestion.Api.Models.Entities.Estructuras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using static Gestion.Api.Helpers.Enums;

namespace Gestion.Api.Models.Entities
{
    public class TipoDato
    {
        public static readonly StructTipoDato Entero = new StructTipoDato
            ((int)EnumTipoDato.Entero, new Guid("BC9C1B1A-4E45-4AA8-B64C-72624A0A2FFF"), "Entero");

        public static readonly StructTipoDato Decimal = new StructTipoDato
            ((int)EnumTipoDato.Decimal, new Guid("FDAFBD38-C57A-4593-955A-DDC232C3F209"), "Decimal");

        public static readonly StructTipoDato Texto = new StructTipoDato
            ((int)EnumTipoDato.Texto, new Guid("AC1515AB-7562-4FC7-A436-EF17586E07D6"), "Texto");

        public static readonly StructTipoDato Tabla = new StructTipoDato
            ((int)EnumTipoDato.Tabla, new Guid("566B3B56-B0B3-422A-92C2-E442D70C95D5"), "Tabla");

        public static readonly StructTipoDato Booleano = new StructTipoDato
            ((int)EnumTipoDato.Booleano, new Guid("1263F9D7-7BDF-43FA-910B-1DBBE01A5B67"), "Booleano");

        public static readonly StructTipoDato Fecha = new StructTipoDato
            ((int)EnumTipoDato.Fecha, new Guid("6EC97364-4DF9-4F2F-896C-DC995C52B1EE"), "Fecha");

        public int Id { get; set; }
        public Guid IdGuid { get; set; }
        public string Tipo { get; set; } = null!;
    }

    public class TipoDatoMap : IEntityTypeConfiguration<TipoDato>
    {
        public void Configure(EntityTypeBuilder<TipoDato> builder)
        {
            builder.ToTable("TipoDato");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.IdGuid).ValueGeneratedOnAdd().IsRequired().HasDefaultValueSql("(newid())");
            builder.Property(t => t.Tipo).HasMaxLength(20).IsRequired();

            builder.HasData(new TipoDato
            {
                Id = TipoDato.Entero.Id,
                IdGuid = TipoDato.Entero.IdGuid,
                Tipo = TipoDato.Entero.Tipo,
            }, new TipoDato
            {
                Id = TipoDato.Decimal.Id,
                IdGuid = TipoDato.Decimal.IdGuid,
                Tipo = TipoDato.Decimal.Tipo,
            }, new TipoDato
            {
                Id = TipoDato.Texto.Id,
                IdGuid = TipoDato.Texto.IdGuid,
                Tipo = TipoDato.Texto.Tipo,
            }, new TipoDato
            {
                Id = TipoDato.Tabla.Id,
                IdGuid = TipoDato.Tabla.IdGuid,
                Tipo = TipoDato.Tabla.Tipo,
            }, new TipoDato
            {
                Id = TipoDato.Booleano.Id,
                IdGuid = TipoDato.Booleano.IdGuid,
                Tipo = TipoDato.Booleano.Tipo,
            }, new TipoDato
            {
                Id = TipoDato.Fecha.Id,
                IdGuid = TipoDato.Fecha.IdGuid,
                Tipo = TipoDato.Fecha.Tipo,
            }
            );
        }
    }
}
