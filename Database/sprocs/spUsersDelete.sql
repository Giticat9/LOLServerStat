BEGIN TRAN
GO

IF OBJECT_ID('[dbo].[spUsersDelete]') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[spUsersDelete]
END
GO

CREATE PROCEDURE [dbo].[spUsersDelete]
(
	@discord_guild_id BIGINT,
	@discord_user_id BIGINT
)
AS
BEGIN
	IF EXISTS(SELECT TOP 1 1 FROM [dbo].[users] WHERE [discord_guild_id] = @discord_guild_id AND [discord_user_id] = @discord_user_id)
	BEGIN
		DECLARE @riot_summoner_id VARCHAR(1000);

		SELECT @riot_summoner_id = [riot_summoner_id] 
		FROM [dbo].[users] 
		WHERE [discord_guild_id] = @discord_guild_id AND [discord_user_id] = @discord_user_id;

		DELETE FROM [dbo].[users]
		WHERE [discord_guild_id] = @discord_guild_id AND [discord_user_id] = @discord_user_id

		DELETE FROM [dbo].[summoners]
		WHERE [riot_summoner_id] = @riot_summoner_id
	END
END

GO
COMMIT TRAN