using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gestion.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEntidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_Entidad_IdEntidad",
                        column: x => x.IdEntidad,
                        principalTable: "Entidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IdTipoUsuario = table.Column<int>(type: "int", nullable: false),
                    IdEntidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Entidad_IdEntidad",
                        column: x => x.IdEntidad,
                        principalTable: "Entidad",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_TipoUsuario_IdTipoUsuario",
                        column: x => x.IdTipoUsuario,
                        principalTable: "TipoUsuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoriaAtributo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoDato = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    EsObligatorio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaAtributo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriaAtributo_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaAtributo_TipoDato_IdTipoDato",
                        column: x => x.IdTipoDato,
                        principalTable: "TipoDato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Expiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revocado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoAtributoValor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdCategoriaAtributo = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoAtributoValor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoAtributoValor_CategoriaAtributo_IdCategoriaAtributo",
                        column: x => x.IdCategoriaAtributo,
                        principalTable: "CategoriaAtributo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductoAtributoValor_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Entidad",
                columns: new[] { "Id", "IdGuid", "Nombre" },
                values: new object[] { 1, new Guid("ed97a159-d788-4f4b-b049-4704ef3e1653"), "Somos Habitos" });

            migrationBuilder.InsertData(
                table: "TipoDato",
                columns: new[] { "Id", "IdGuid", "Tipo" },
                values: new object[,]
                {
                    { 1, new Guid("bc9c1b1a-4e45-4aa8-b64c-72624a0a2fff"), "Entero" },
                    { 2, new Guid("fdafbd38-c57a-4593-955a-ddc232c3f209"), "Decimal" },
                    { 3, new Guid("ac1515ab-7562-4fc7-a436-ef17586e07d6"), "Texto" },
                    { 4, new Guid("566b3b56-b0b3-422a-92c2-e442d70c95d5"), "Tabla" },
                    { 5, new Guid("1263f9d7-7bdf-43fa-910b-1dbbe01a5b67"), "Booleano" },
                    { 6, new Guid("6ec97364-4df9-4f2f-896c-dc995c52b1ee"), "Fecha" }
                });

            migrationBuilder.InsertData(
                table: "TipoUsuario",
                columns: new[] { "Id", "Descripcion", "IdGuid" },
                values: new object[,]
                {
                    { 1, "Aministrador", new Guid("31c76205-6ddd-43a6-8bc4-0c748ddfcffa") },
                    { 2, "Empleado", new Guid("c1a321cc-7a0a-4079-a6d2-d17f9f045fd3") },
                    { 3, "Usuario", new Guid("ad9b7732-2a84-4c78-9a47-717d5b47eef0") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_IdEntidad",
                table: "Categoria",
                column: "IdEntidad");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaAtributo_IdCategoria",
                table: "CategoriaAtributo",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaAtributo_IdTipoDato",
                table: "CategoriaAtributo",
                column: "IdTipoDato");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdCategoria",
                table: "Producto",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoAtributoValor_IdCategoriaAtributo",
                table: "ProductoAtributoValor",
                column: "IdCategoriaAtributo");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoAtributoValor_IdProducto",
                table: "ProductoAtributoValor",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IdUsuario",
                table: "RefreshToken",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEntidad",
                table: "Usuario",
                column: "IdEntidad");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdTipoUsuario",
                table: "Usuario",
                column: "IdTipoUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoAtributoValor");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "CategoriaAtributo");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "TipoDato");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "TipoUsuario");

            migrationBuilder.DropTable(
                name: "Entidad");
        }
    }
}
