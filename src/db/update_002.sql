-- --------------------------------------------------
-- Changes in update 2
-- --------------------------------------------------

-- Table Contacts
ALTER TABLE [dbo].[Contacts]
DROP COLUMN [AccountId];

ALTER TABLE [dbo].[Contacts]
DROP COLUMN [PublicUserId];

-- --------------------------------------------------
-- Set DB version
-- --------------------------------------------------

UPDATE [dbo].[SystemValues]
    SET Value = 2 where id = N'e099a5b7-e033-eb11-a9e7-e0d55ee540e6';
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
