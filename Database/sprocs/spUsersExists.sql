BEGIN TRAN
GO

IF OBJECT_ID('[dbo].[spUsersExistse]') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[spUsersExists]
END
GO

CREATE PROCEDURE [dbo].[spUsersExists]
(
	@discord_guild_id BIGINT,
	@discord_user_id BIGINT
)
AS
BEGIN
	SELECT CAST(
		CASE WHEN EXISTS(
			SELECT TOP 1 1
			FROM [dbo].[users] 
			WHERE [discord_guild_id] = @discord_guild_id AND [discord_user_id] = @discord_user_id)
		THEN 1
		ELSE 0
		END
		AS BIT
	)
END

GO
COMMIT TRAN;