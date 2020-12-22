-- --------------------------------------------------
-- Changes in update 3
-- --------------------------------------------------

-- Modifying foreign key on [BuildingInfoId] in table 'Estates'
ALTER TABLE [dbo].[Estates]
    DROP CONSTRAINT [FK_EstateBuildingInfo];
GO

ALTER TABLE [dbo].[Estates]
ADD CONSTRAINT [FK_EstateBuildingInfo]
    FOREIGN KEY ([BuildingInfoId])
    REFERENCES [dbo].[BuildingInfoes]
        ([Id])
    ON DELETE SET NULL ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Set DB version
-- --------------------------------------------------

UPDATE [dbo].[SystemValues]
    SET Value = 2 where id = N'e099a5b7-e033-eb11-a9e7-e0d55ee540e6';
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------