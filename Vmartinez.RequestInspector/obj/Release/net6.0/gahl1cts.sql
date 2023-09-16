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

CREATE TABLE [HttpContext] (
    [Id] int NOT NULL IDENTITY,
    [Application] nvarchar(255) NULL,
    [Path] nvarchar(max) NULL,
    [Method] nvarchar(max) NULL,
    [RequestHeader] nvarchar(max) NULL,
    [RequestBody] nvarchar(max) NULL,
    [ResponseHeader] nvarchar(max) NULL,
    [ResponseBody] nvarchar(max) NULL,
    [RequestDateRegistration] datetime2 NOT NULL,
    [ResponseDateRegistration] datetime2 NOT NULL,
    CONSTRAINT [PK_HttpContext] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230221180053_initial', N'6.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230717223930_script', N'6.0.13');
GO

COMMIT;
GO

