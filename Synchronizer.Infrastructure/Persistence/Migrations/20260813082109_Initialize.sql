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

