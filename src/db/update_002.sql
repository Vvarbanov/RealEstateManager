ALTER TABLE [dbo].[Estates]
    ADD [UpdateDate] datetime NOT  NULL default CURRENT_TIMESTAMP;
GO

UPDATE [dbo].[SystemValues]
	SET Value=2 where id=N'e099a5b7-e033-eb11-a9e7-e0d55ee540e6';
GO