IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Entidad] (
    [Id] int NOT NULL IDENTITY,
    [IdGuid] uniqueidentifier NOT NULL,
    [Nombre] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Entidad] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoDato] (
    [Id] int NOT NULL IDENTITY,
    [IdGuid] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [Tipo] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_TipoDato] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TipoUsuario] (
    [Id] int NOT NULL IDENTITY,
    [IdGuid] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [Descripcion] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_TipoUsuario] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categoria] (
    [Id] int NOT NULL IDENTITY,
    [IdGuid] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [Descripcion] nvarchar(max) NOT NULL,
    [Icono] nvarchar(max) NULL,
    [IdEntidad] int NOT NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Categoria_Entidad_IdEntidad] FOREIGN KEY ([IdEntidad]) REFERENCES [Entidad] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Usuario] (
    [Id] int NOT NULL IDENTITY,
    [IdGuid] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [Username] nvarchar(100) NOT NULL,
    [Password] nvarchar(200) NOT NULL,
    [PasswordHash] nvarchar(200) NOT NULL,
    [Nombre] nvarchar(100) NOT NULL,
    [Apellido] nvarchar(100) NOT NULL,
    [FechaAlta] datetime2 NOT NULL DEFAULT (GETDATE()),
    [IdTipoUsuario] int NOT NULL,
    [IdEntidad] int NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Usuario_Entidad_IdEntidad] FOREIGN KEY ([IdEntidad]) REFERENCES [Entidad] ([Id]),
    CONSTRAINT [FK_Usuario_TipoUsuario_IdTipoUsuario] FOREIGN KEY ([IdTipoUsuario]) REFERENCES [TipoUsuario] ([Id])
);
GO

CREATE TABLE [CategoriaAtributo] (
    [Id] int NOT NULL IDENTITY,
    [IdGuid] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [Nombre] nvarchar(max) NOT NULL,
    [IdTipoDato] int NOT NULL,
    [IdCategoria] int NOT NULL,
    [EsObligatorio] bit NOT NULL,
    CONSTRAINT [PK_CategoriaAtributo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CategoriaAtributo_Categoria_IdCategoria] FOREIGN KEY ([IdCategoria]) REFERENCES [Categoria] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoriaAtributo_TipoDato_IdTipoDato] FOREIGN KEY ([IdTipoDato]) REFERENCES [TipoDato] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Producto] (
    [Id] int NOT NULL IDENTITY,
    [IdGuid] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [Nombre] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NULL,
    [IdCategoria] int NOT NULL,
    CONSTRAINT [PK_Producto] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Producto_Categoria_IdCategoria] FOREIGN KEY ([IdCategoria]) REFERENCES [Categoria] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RefreshToken] (
    [Id] int NOT NULL IDENTITY,
    [IdGuid] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [IdUsuario] int NOT NULL,
    [Token] nvarchar(500) NOT NULL,
    [Expiracion] datetime2 NOT NULL,
    [Revocado] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshToken_Usuario_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductoAtributoValor] (
    [Id] int NOT NULL IDENTITY,
    [IdGuid] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [IdProducto] int NOT NULL,
    [IdCategoriaAtributo] int NOT NULL,
    [Valor] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ProductoAtributoValor] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductoAtributoValor_CategoriaAtributo_IdCategoriaAtributo] FOREIGN KEY ([IdCategoriaAtributo]) REFERENCES [CategoriaAtributo] ([Id]),
    CONSTRAINT [FK_ProductoAtributoValor_Producto_IdProducto] FOREIGN KEY ([IdProducto]) REFERENCES [Producto] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IdGuid', N'Nombre') AND [object_id] = OBJECT_ID(N'[Entidad]'))
    SET IDENTITY_INSERT [Entidad] ON;
INSERT INTO [Entidad] ([Id], [IdGuid], [Nombre])
VALUES (1, 'ed97a159-d788-4f4b-b049-4704ef3e1653', N'Somos Habitos');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IdGuid', N'Nombre') AND [object_id] = OBJECT_ID(N'[Entidad]'))
    SET IDENTITY_INSERT [Entidad] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IdGuid', N'Tipo') AND [object_id] = OBJECT_ID(N'[TipoDato]'))
    SET IDENTITY_INSERT [TipoDato] ON;
INSERT INTO [TipoDato] ([Id], [IdGuid], [Tipo])
VALUES (1, 'bc9c1b1a-4e45-4aa8-b64c-72624a0a2fff', N'Entero'),
(2, 'fdafbd38-c57a-4593-955a-ddc232c3f209', N'Decimal'),
(3, 'ac1515ab-7562-4fc7-a436-ef17586e07d6', N'Texto'),
(4, '566b3b56-b0b3-422a-92c2-e442d70c95d5', N'Tabla'),
(5, '1263f9d7-7bdf-43fa-910b-1dbbe01a5b67', N'Booleano'),
(6, '6ec97364-4df9-4f2f-896c-dc995c52b1ee', N'Fecha');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IdGuid', N'Tipo') AND [object_id] = OBJECT_ID(N'[TipoDato]'))
    SET IDENTITY_INSERT [TipoDato] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descripcion', N'IdGuid') AND [object_id] = OBJECT_ID(N'[TipoUsuario]'))
    SET IDENTITY_INSERT [TipoUsuario] ON;
INSERT INTO [TipoUsuario] ([Id], [Descripcion], [IdGuid])
VALUES (1, N'Aministrador', '31c76205-6ddd-43a6-8bc4-0c748ddfcffa'),
(2, N'Empleado', 'c1a321cc-7a0a-4079-a6d2-d17f9f045fd3'),
(3, N'Usuario', 'ad9b7732-2a84-4c78-9a47-717d5b47eef0');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Descripcion', N'IdGuid') AND [object_id] = OBJECT_ID(N'[TipoUsuario]'))
    SET IDENTITY_INSERT [TipoUsuario] OFF;
GO

CREATE INDEX [IX_Categoria_IdEntidad] ON [Categoria] ([IdEntidad]);
GO

CREATE INDEX [IX_CategoriaAtributo_IdCategoria] ON [CategoriaAtributo] ([IdCategoria]);
GO

CREATE INDEX [IX_CategoriaAtributo_IdTipoDato] ON [CategoriaAtributo] ([IdTipoDato]);
GO

CREATE INDEX [IX_Producto_IdCategoria] ON [Producto] ([IdCategoria]);
GO

CREATE INDEX [IX_ProductoAtributoValor_IdCategoriaAtributo] ON [ProductoAtributoValor] ([IdCategoriaAtributo]);
GO

CREATE INDEX [IX_ProductoAtributoValor_IdProducto] ON [ProductoAtributoValor] ([IdProducto]);
GO

CREATE UNIQUE INDEX [IX_RefreshToken_IdUsuario] ON [RefreshToken] ([IdUsuario]);
GO

CREATE INDEX [IX_Usuario_IdEntidad] ON [Usuario] ([IdEntidad]);
GO

CREATE INDEX [IX_Usuario_IdTipoUsuario] ON [Usuario] ([IdTipoUsuario]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251111185918_InitialMigration', N'8.0.21');
GO

COMMIT;
GO

