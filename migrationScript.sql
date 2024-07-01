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

CREATE TABLE [Restaurants] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(250) NOT NULL,
    [Description] nvarchar(2000) NULL,
    [Location] nvarchar(250) NOT NULL,
    [Type] int NOT NULL,
    [Phone] nvarchar(15) NULL,
    [Email] nvarchar(100) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    CONSTRAINT [PK_Restaurants] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Menus] (
    [Id] int NOT NULL IDENTITY,
    [RestaurantId] int NOT NULL,
    [MenuType] int NOT NULL,
    [Name] nvarchar(250) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    CONSTRAINT [PK_Menus] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Menus_Restaurants_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [MenuItems] (
    [Id] int NOT NULL IDENTITY,
    [MenuId] int NOT NULL,
    [Name] nvarchar(250) NOT NULL,
    [Description] nvarchar(2000) NULL,
    [Price] decimal(18,2) NOT NULL,
    [ImagePath] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    CONSTRAINT [PK_MenuItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MenuItems_Menus_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Menus] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MenuItems_MenuId] ON [MenuItems] ([MenuId]);
GO

CREATE INDEX [IX_Menus_RestaurantId] ON [Menus] ([RestaurantId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240701163030_Initilize_Database', N'8.0.6');
GO

COMMIT;
GO

