
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/26/2020 17:16:11
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

IF OBJECT_ID(N'[dbo].[FK_EstateAgentEstate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EstateAgents] DROP CONSTRAINT [FK_EstateAgentEstate];
GO
IF OBJECT_ID(N'[dbo].[FK_EstateAgentAgent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EstateAgents] DROP CONSTRAINT [FK_EstateAgentAgent];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactEstate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_ContactEstate];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactAgent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_ContactAgent];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactPublicUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_ContactPublicUser];
GO
IF OBJECT_ID(N'[dbo].[FK_FileContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_FileContact];
GO
IF OBJECT_ID(N'[dbo].[FK_FileEstate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_FileEstate];
GO
IF OBJECT_ID(N'[dbo].[FK_EstateBuildingInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Estates] DROP CONSTRAINT [FK_EstateBuildingInfo];
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
IF OBJECT_ID(N'[dbo].[Agents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Agents];
GO
IF OBJECT_ID(N'[dbo].[EstateAgents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EstateAgents];
GO
IF OBJECT_ID(N'[dbo].[PublicUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PublicUsers];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
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
    [UpdateDate] datetime  NOT NULL
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

-- Creating table 'Agents'
CREATE TABLE [dbo].[Agents] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [Username] nvarchar(max)  NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL,
    [PasswordSalt] nvarchar(max)  NOT NULL,
    [HashedPassword] nvarchar(max)  NOT NULL,
    [ForgottenPasswordToken] nvarchar(max)  NULL
);
GO

-- Creating table 'EstateAgents'
CREATE TABLE [dbo].[EstateAgents] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [EstateId] uniqueidentifier  NOT NULL,
    [AgentId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'PublicUsers'
CREATE TABLE [dbo].[PublicUsers] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [Username] nvarchar(max)  NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL,
    [PasswordSalt] nvarchar(max)  NOT NULL,
    [HashedPassword] nvarchar(max)  NOT NULL,
    [ForgottenPasswordToken] nvarchar(max)  NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [DateTime] datetime  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Outcome] nvarchar(max)  NOT NULL,
    [EstateId] uniqueidentifier  NOT NULL,
    [AgentId] uniqueidentifier  NOT NULL,
    [PublicUserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [PathOnFileSystem] nvarchar(max)  NOT NULL,
    [DisplayName] nvarchar(max)  NULL,
    [ContactId] uniqueidentifier  NULL,
    [EstateId] uniqueidentifier  NULL
);
GO

-- Creating table 'SystemValues'
CREATE TABLE [dbo].[SystemValues] (
    [Id] uniqueidentifier  NOT NULL DEFAULT newsequentialid(),
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
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

-- Creating primary key on [Id] in table 'Agents'
ALTER TABLE [dbo].[Agents]
ADD CONSTRAINT [PK_Agents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EstateAgents'
ALTER TABLE [dbo].[EstateAgents]
ADD CONSTRAINT [PK_EstateAgents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PublicUsers'
ALTER TABLE [dbo].[PublicUsers]
ADD CONSTRAINT [PK_PublicUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SystemValues'
ALTER TABLE [dbo].[SystemValues]
ADD CONSTRAINT [PK_SystemValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EstateId] in table 'EstateAgents'
ALTER TABLE [dbo].[EstateAgents]
ADD CONSTRAINT [FK_EstateAgentEstate]
    FOREIGN KEY ([EstateId])
    REFERENCES [dbo].[Estates]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EstateAgentEstate'
CREATE INDEX [IX_FK_EstateAgentEstate]
ON [dbo].[EstateAgents]
    ([EstateId]);
GO

-- Creating foreign key on [AgentId] in table 'EstateAgents'
ALTER TABLE [dbo].[EstateAgents]
ADD CONSTRAINT [FK_EstateAgentAgent]
    FOREIGN KEY ([AgentId])
    REFERENCES [dbo].[Agents]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EstateAgentAgent'
CREATE INDEX [IX_FK_EstateAgentAgent]
ON [dbo].[EstateAgents]
    ([AgentId]);
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

-- Creating foreign key on [AgentId] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [FK_ContactAgent]
    FOREIGN KEY ([AgentId])
    REFERENCES [dbo].[Agents]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactAgent'
CREATE INDEX [IX_FK_ContactAgent]
ON [dbo].[Contacts]
    ([AgentId]);
GO

-- Creating foreign key on [PublicUserId] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [FK_ContactPublicUser]
    FOREIGN KEY ([PublicUserId])
    REFERENCES [dbo].[PublicUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactPublicUser'
CREATE INDEX [IX_FK_ContactPublicUser]
ON [dbo].[Contacts]
    ([PublicUserId]);
GO

-- Creating foreign key on [ContactId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_FileContact]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE SET NULL ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileContact'
CREATE INDEX [IX_FK_FileContact]
ON [dbo].[Files]
    ([ContactId]);
GO

-- Creating foreign key on [EstateId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_FileEstate]
    FOREIGN KEY ([EstateId])
    REFERENCES [dbo].[Estates]
        ([Id])
    ON DELETE SET NULL ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileEstate'
CREATE INDEX [IX_FK_FileEstate]
ON [dbo].[Files]
    ([EstateId]);
GO

-- Creating foreign key on [BuildingInfoId] in table 'Estates'
ALTER TABLE [dbo].[Estates]
ADD CONSTRAINT [FK_EstateBuildingInfo]
    FOREIGN KEY ([BuildingInfoId])
    REFERENCES [dbo].[BuildingInfoes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating unique non-clustered index for FOREIGN KEY 'FK_EstateBuildingInfo'
CREATE UNIQUE NONCLUSTERED INDEX [IX_FK_EstateBuildingInfo]
ON [dbo].[Estates]
    ([BuildingInfoId])
WHERE [BuildingInfoId] IS NOT NULL;
GO

-- --------------------------------------------------
-- Set DB version
-- --------------------------------------------------

INSERT INTO [dbo].[SystemValues]
    VALUES (N'e099a5b7-e033-eb11-a9e7-e0d55ee540e6', N'Version', N'3');
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
