-- --------------------------------------------------
-- Changes in update 3
-- --------------------------------------------------

-- Creating foreign key on [BuildingInfoId] in table 'Estates'
ALTER TABLE [dbo].[Estates]
    DROP CONSTRAINT [FK_EstateBuildingInfo];
GO

ALTER TABLE [dbo].[Estates]
ADD CONSTRAINT [FK_EstateBuildingInfo]
    FOREIGN KEY ([BuildingInfoId])
    REFERENCES [dbo].[BuildingInfoes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO
-- --------------------------------------------------
-- Set DB version
-- --------------------------------------------------

UPDATE [dbo].[SystemValues]
	SET Value = 3 where id = N'e099a5b7-e033-eb11-a9e7-e0d55ee540e6';
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
