BEGIN TRAN
GO

IF OBJECT_ID('[dbo].[spChampionsGet]') IS NOT NULL
	DROP PROCEDURE [dbo].[spChampionsGet]
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM [sys].[types] WHERE [name] = 'ArrayOfBigint')
BEGIN
	CREATE TYPE [dbo].[ArrayOfBigint] AS TABLE
	(
		[value] BIGINT
	)
END

GO
COMMIT TRAN