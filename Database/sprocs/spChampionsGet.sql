BEGIN TRAN
GO

IF OBJECT_ID('[dbo].[spChampionsGet]') IS NOT NULL
	DROP PROCEDURE [dbo].[spChampionsGet]
GO

CREATE PROCEDURE [dbo].[spChampionsGet]
(
	@champions_ids [dbo].[ArrayOfBigint] READONLY
)
AS
BEGIN
	SELECT c.[code]
		,c.[key]
		,c.[name]
		,c.[originalName]
		,c.[updated_date]
	FROM [dbo].[champions] c
	WHERE (
		NOT EXISTS (SELECT TOP 1 1 FROM @champions_ids)
			OR c.[code] IN (SELECT [value] FROM @champions_ids)
	) AND c.[is_deleted] = 0
END

GO
COMMIT TRAN
