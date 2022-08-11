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

CREATE TABLE [Despesas] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NOT NULL,
    [valor] int NOT NULL,
    [Data] datetime2 NOT NULL,
    CONSTRAINT [PK_Despesas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Receitas] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NOT NULL,
    [valor] int NOT NULL,
    [Data] datetime2 NOT NULL,
    CONSTRAINT [PK_Receitas] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220802003650_Initial', N'6.0.7');
GO

COMMIT;
GO

