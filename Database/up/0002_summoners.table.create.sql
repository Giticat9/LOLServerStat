BEGIN TRAN
GO

IF NOT EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE id = OBJECT_ID('[dbo].[summoners]'))
BEGIN
	CREATE TABLE [dbo].[summoners]
	(
		[id] BIGINT PRIMARY KEY IDENTITY(1,1),
		[riot_summoner_id] VARCHAR(1000) NOT NULL,
		[name] VARCHAR(1000) NOT NULL,
		[level] BIGINT NOT NULL,
		[queue_type] VARCHAR(1000) NOT NULL,
		[tier] VARCHAR(10) NOT NULL,
		[rank] VARCHAR(10) NOT NULL,
		[league_points] BIGINT NOT NULL,
		[wins] BIGINT NOT NULL,
		[losses] BIGINT NOT NULL,
		[veteran] BIT NOT NULL DEFAULT(0),
		[inactive] BIT NOT NULL DEFAULT(0),
		[fresh_blood] BIT NOT NULL DEFAULT(0),
		[hot_streak] BIT NOT NULL DEFAULT(0),
	)
END;

GO
COMMIT TRAN