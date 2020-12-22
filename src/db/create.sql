
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/11/2020 18:38:44
-- Generated from EDMX file: D:\Code\MyRepo\RealEstateManager\src\RealEstateManager\Models\Data\RealEstateManagerDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER ON;
GO
USE [RealEstateManager];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EstateAccountEstate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EstateAccounts] DROP CONSTRAINT [FK_EstateAccountEstate];
GO
IF OBJECT_ID(N'[dbo].[FK_EstateAccountAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EstateAccounts] DROP CONSTRAINT [FK_EstateAccountAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactEstate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_ContactEstate];
GO
IF OBJECT_ID(N'[dbo].[FK_EstateBuildingInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Estates] DROP CONSTRAINT [FK_EstateBuildingInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountContactAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactAccounts] DROP CONSTRAINT [FK_AccountContactAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactContactAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactAccounts] DROP CONSTRAINT [FK_ContactContactAccount];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Estates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Estates];
GO
IF OBJECT_ID(N'[dbo].[BuildingInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BuildingInfoes];
GO
IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[EstateAccounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EstateAccounts];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[SystemValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemValues];
GO
IF OBJECT_ID(N'[dbo].[ContactAccounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactAccounts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Estates'
CREATE TABLE [dbo].[Estates] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [Name] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Status] int  NOT NULL,
    [PublicDescription] nvarchar(max)  NULL,
    [PrivateDescription] nvarchar(max)  NULL,
    [Area] float  NOT NULL,
    [BuildingInfoId] uniqueidentifier  NULL,
    [UpdateDate] datetime  NOT NULL DEFAULT GETDATE(),
    [FilePathsCSV] nvarchar(max)  NULL
);
GO

-- Creating table 'BuildingInfoes'
CREATE TABLE [dbo].[BuildingInfoes] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [View] int  NOT NULL,
    [Act16] bit  NOT NULL,
    [Floors] int  NOT NULL,
    [Bedrooms] int  NOT NULL,
    [Bathrooms] int  NOT NULL,
    [Balconies] int  NOT NULL,
    [Garages] int  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [Type] int  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL,
    [PasswordSalt] nvarchar(max)  NOT NULL,
    [HashedPassword] nvarchar(max)  NOT NULL,
    [ForgottenPasswordToken] nvarchar(max)  NULL
);
GO

-- Creating table 'EstateAccounts'
CREATE TABLE [dbo].[EstateAccounts] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [EstateId] uniqueidentifier  NOT NULL,
    [AccountId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [DateTime] datetime  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Outcome] nvarchar(max)  NOT NULL,
    [EstateId] uniqueidentifier  NOT NULL,
    [AccountId] uniqueidentifier  NOT NULL,
    [PublicUserId] uniqueidentifier  NOT NULL,
    [FilePathsCSV] nvarchar(max)  NULL
);
GO

-- Creating table 'SystemValues'
CREATE TABLE [dbo].[SystemValues] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ContactAccounts'
CREATE TABLE [dbo].[ContactAccounts] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [AccountId] uniqueidentifier  NOT NULL,
    [ContactId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Estates'
ALTER TABLE [dbo].[Estates]
ADD CONSTRAINT [PK_Estates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BuildingInfoes'
ALTER TABLE [dbo].[BuildingInfoes]
ADD CONSTRAINT [PK_BuildingInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EstateAccounts'
ALTER TABLE [dbo].[EstateAccounts]
ADD CONSTRAINT [PK_EstateAccounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SystemValues'
ALTER TABLE [dbo].[SystemValues]
ADD CONSTRAINT [PK_SystemValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContactAccounts'
ALTER TABLE [dbo].[ContactAccounts]
ADD CONSTRAINT [PK_ContactAccounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EstateId] in table 'EstateAccounts'
ALTER TABLE [dbo].[EstateAccounts]
ADD CONSTRAINT [FK_EstateAccountEstate]
    FOREIGN KEY ([EstateId])
    REFERENCES [dbo].[Estates]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EstateAccountEstate'
CREATE INDEX [IX_FK_EstateAccountEstate]
ON [dbo].[EstateAccounts]
    ([EstateId]);
GO

-- Creating foreign key on [AccountId] in table 'EstateAccounts'
ALTER TABLE [dbo].[EstateAccounts]
ADD CONSTRAINT [FK_EstateAccountAccount]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EstateAccountAccount'
CREATE INDEX [IX_FK_EstateAccountAccount]
ON [dbo].[EstateAccounts]
    ([AccountId]);
GO

-- Creating foreign key on [EstateId] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [FK_ContactEstate]
    FOREIGN KEY ([EstateId])
    REFERENCES [dbo].[Estates]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactEstate'
CREATE INDEX [IX_FK_ContactEstate]
ON [dbo].[Contacts]
    ([EstateId]);
GO

-- Creating foreign key on [BuildingInfoId] in table 'Estates'
ALTER TABLE [dbo].[Estates]
ADD CONSTRAINT [FK_EstateBuildingInfo]
    FOREIGN KEY ([BuildingInfoId])
    REFERENCES [dbo].[BuildingInfoes]
        ([Id])
    ON DELETE SET NULL ON UPDATE NO ACTION;
GO

-- Creating unique non-clustered index for FOREIGN KEY 'FK_EstateBuildingInfo'
CREATE UNIQUE NONCLUSTERED INDEX [IX_FK_EstateBuildingInfo]
ON [dbo].[Estates]
    ([BuildingInfoId])
WHERE [BuildingInfoId] IS NOT NULL;
GO

-- Creating foreign key on [AccountId] in table 'ContactAccounts'
ALTER TABLE [dbo].[ContactAccounts]
ADD CONSTRAINT [FK_AccountContactAccount]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountContactAccount'
CREATE INDEX [IX_FK_AccountContactAccount]
ON [dbo].[ContactAccounts]
    ([AccountId]);
GO

-- Creating foreign key on [ContactId] in table 'ContactAccounts'
ALTER TABLE [dbo].[ContactAccounts]
ADD CONSTRAINT [FK_ContactContactAccount]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactContactAccount'
CREATE INDEX [IX_FK_ContactContactAccount]
ON [dbo].[ContactAccounts]
    ([ContactId]);
GO

-- --------------------------------------------------
-- Set DB version
-- --------------------------------------------------

INSERT INTO [dbo].[SystemValues]
    VALUES (N'e099a5b7-e033-eb11-a9e7-e0d55ee540e6', N'Version', N'2');
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
