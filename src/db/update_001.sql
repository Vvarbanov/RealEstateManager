-- --------------------------------------------------
-- Changes in update 1
-- --------------------------------------------------

ALTER TABLE [dbo].[Agents]
    ADD [ForgottenPasswordToken] nvarchar(max)  NULL;
GO

ALTER TABLE [dbo].[PublicUsers]
    ADD [ForgottenPasswordToken] nvarchar(max)  NULL;
GO

-- --------------------------------------------------
-- Set DB version
-- --------------------------------------------------

INSERT INTO [dbo].[SystemValues]
    VALUES (N'e099a5b7-e033-eb11-a9e7-e0d55ee540e6', N'Version', N'1');
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
