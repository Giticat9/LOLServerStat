BEGIN TRAN
GO

IF NOT EXISTS (SELECT * FROM [sys].[sysobjects] WHERE id = OBJECT_ID('[dbo].[users]'))
BEGIN
	CREATE TABLE [dbo].[users] 
	(
		[id] BIGINT PRIMARY KEY IDENTITY(1,1),
		[discord_user_id] BIGINT NOT NULL,
		[discord_guild_id] BIGINT NOT NULL,
		[riot_summoner_id] VARCHAR(1000) NOT NULL,
		[updated_date] DATETIME NOT NULL DEFAULT(SYSDATETIME()),
		[is_deleted] BIT NOT NULL DEFAULT 0,
	)
END

GO
COMMIT TRAN