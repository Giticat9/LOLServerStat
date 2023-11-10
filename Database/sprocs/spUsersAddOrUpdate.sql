BEGIN TRAN
GO

IF OBJECT_ID('[dbo].[spUsersAddOrUpdate]') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[spUsersAddOrUpdate];
END
GO

CREATE PROCEDURE [dbo].[spUsersAddOrUpdate]
(
	@discord_guild_id BIGINT,
	@discord_user_id BIGINT,
	@riot_summoner_id VARCHAR(1000),
	@summoner_models [dbo].[SummonerType] READONLY
)
AS
BEGIN
	MERGE [dbo].[users] AS t
	USING ( 
		VALUES (@discord_guild_id, @discord_user_id, @riot_summoner_id)
	) AS s (discord_guild_id, discord_user_id, riot_summoner_id)
	ON t.[discord_guild_id] = s.[discord_guild_id]
		AND t.[discord_user_id] = s.[discord_user_id]
		AND t.[riot_summoner_id] = s.[riot_summoner_id]
	WHEN NOT MATCHED BY TARGET
	THEN 
		INSERT ([discord_guild_id], [discord_user_id], [riot_summoner_id])
		VALUES (s.[discord_guild_id], s.[discord_user_id], s.[riot_summoner_id])
	WHEN MATCHED
	THEN
		UPDATE SET
			t.[discord_guild_id] = s.[discord_guild_id],
			t.[discord_user_id] = s.[discord_user_id],
			t.[riot_summoner_id] = s.[riot_summoner_id],
			t.[updated_date] = SYSDATETIME();

	IF EXISTS (SELECT TOP 1 1 FROM @summoner_models)
	BEGIN
		MERGE [dbo].[summoners] AS t
		USING @summoner_models AS s
		ON t.[riot_summoner_id] = s.[riot_summoner_id]
			AND t.[queue_type] != s.[queue_type]
		WHEN NOT MATCHED BY TARGET
		THEN
			INSERT (
				[riot_summoner_id],
				[name],
				[level],
				[queue_type],
				[tier],
				[rank],
				[league_points],
				[wins],
				[losses],
				[veteran],
				[inactive],
				[fresh_blood],
				[hot_streak]
			)
			VALUES (
				s.[riot_summoner_id],
				s.[name],
				s.[level],
				s.[queue_type],
				s.[tier],
				s.[rank],
				s.[league_points],
				s.[wins],
				s.[losses],
				s.[veteran],
				s.[inactive],
				s.[fresh_blood],
				s.[hot_streak]
			)
		WHEN MATCHED
		THEN
			UPDATE SET
				t.[riot_summoner_id] = s.[riot_summoner_id],
				t.[name] = s.[name],
				t.[level] = s.[level],
				t.[queue_type] = s.[queue_type],
				t.[tier] = s.[tier],
				t.[rank] = s.[rank],
				t.[league_points] = s.[league_points],
				t.[wins] = s.[wins],
				t.[losses] = s.[losses],
				t.[veteran] = s.[veteran],
				t.[inactive] = s.[inactive],
				t.[fresh_blood] = s.[fresh_blood],
				t.[hot_streak] = s.[hot_streak];
		END
END

GO
COMMIT TRAN