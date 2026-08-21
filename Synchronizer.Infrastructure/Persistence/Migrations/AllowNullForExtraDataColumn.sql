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
CREATE TABLE [Providers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NOT NULL,
    [EndpointUrl] nvarchar(500) NOT NULL,
    [EndpointTimeout] int NOT NULL,
    [ScheduleCron] nvarchar(100) NOT NULL,
    [ScheduleMaxRetryCount] int NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Providers] PRIMARY KEY ([Id])
);

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(300) NOT NULL,
    [Price] decimal(18,4) NOT NULL,
    [VAT] decimal(5,2) NOT NULL,
    [ExtraData] JSON NOT NULL,
    [IsActive] bit NOT NULL,
    [ProviderId] int NOT NULL,
    [ExternalId] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id])
);

CREATE UNIQUE INDEX [IX_Product_ProviderId_ExternalId] ON [Product] ([ProviderId], [ExternalId]);

CREATE UNIQUE INDEX [IX_Providers_Name] ON [Providers] ([Name]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260813082109_Initialize', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'ExtraData');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Product] ALTER COLUMN [ExtraData] JSON NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260820131910_AllowNullForExtraDataColumn', N'10.0.11');

COMMIT;
GO

