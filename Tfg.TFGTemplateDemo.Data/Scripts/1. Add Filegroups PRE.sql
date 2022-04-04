DECLARE	@DatabaseName varchar(100),
		@FilePath varchar(300),
		@Index tinyint,
		@NumberOfFiles tinyint,
		@Schema varchar(100),
		@ReconSchema varchar(100),
		@Suffix varchar(5),
		@Iteration varchar(5),
		@QuoteSchema varchar(max)

SET @DatabaseName = DB_Name()
--SET @FilePath = (SELECT TOP 1 SubString(df.Physical_Name, 1, Len(df.Physical_Name) - CharIndex('\', Reverse(df.Physical_Name)) + 1) FROM sys.Database_Files df JOIN sys.FileGroups fg ON df.Data_Space_Id = fg.Data_Space_Id WHERE fg.Filegroup_Guid IS NULL)
SELECT @FilePath=cast(SERVERPROPERTY('InstanceDefaultDataPath') as varchar(300))
SET @Index = 0
SET @NumberOfFiles = 4
SET @Schema = 'Tfg'
SET @QuoteSchema = QUOTENAME(@Schema)

IF NOT EXISTS (SELECT * FROM sys.FileGroups WHERE [Name] = @Schema) BEGIN
	EXECUTE ('ALTER DATABASE ' + @DatabaseName + ' ADD FILEGROUP ' + @Schema)
	EXECUTE ('ALTER DATABASE ' + @DatabaseName + ' ADD FILEGROUP ' + @Schema + '_NCI')
	EXECUTE ('ALTER DATABASE ' + @DatabaseName + ' ADD FILEGROUP ' + @Schema + '_LOB')

	WHILE @Index < @NumberOfFiles BEGIN
		SET @Index = @Index + 1

		SET @Iteration = Right('00' + Cast(@Index AS varchar(2)), 2)
		SET @Suffix = @Iteration
		EXECUTE ('ALTER DATABASE ' + @DatabaseName + ' ADD FILE (NAME = ' + @DatabaseName + @Schema + @Suffix + ', FILENAME = ''' + @FilePath + @DatabaseName + @Schema + @Suffix + '.ndf'', SIZE = 250MB, MAXSIZE = UNLIMITED, FILEGROWTH = 250MB) TO FILEGROUP ' + @Schema)
		SET @Suffix = 'NCI' + @Iteration
		EXECUTE ('ALTER DATABASE ' + @DatabaseName + ' ADD FILE (NAME = ' + @DatabaseName + @Schema + @Suffix + ', FILENAME = ''' + @FilePath + @DatabaseName + @Schema + @Suffix + '.ndf'', SIZE = 250MB, MAXSIZE = UNLIMITED, FILEGROWTH = 250MB) TO FILEGROUP ' + @Schema + '_NCI')
		SET @Suffix = 'LOB' + @Iteration
		EXECUTE ('ALTER DATABASE ' + @DatabaseName + ' ADD FILE (NAME = ' + @DatabaseName + @Schema + @Suffix + ', FILENAME = ''' + @FilePath + @DatabaseName + @Schema + @Suffix + '.ndf'', SIZE = 250MB, MAXSIZE = UNLIMITED, FILEGROWTH = 250MB) TO FILEGROUP ' + @Schema + '_LOB')
	END
END


IF NOT EXISTS (SELECT * FROM sys.FileGroups WHERE [Name] = @Schema AND Is_Default = 1) BEGIN
	EXECUTE ('ALTER DATABASE ' + @DatabaseName + ' MODIFY FILEGROUP ' + @Schema + ' DEFAULT')
END

IF (NOT EXISTS (SELECT * FROM sys.schemas WHERE name = @Schema)) 
BEGIN
	EXEC ('CREATE SCHEMA ' + @QuoteSchema  + ';')
END

GO