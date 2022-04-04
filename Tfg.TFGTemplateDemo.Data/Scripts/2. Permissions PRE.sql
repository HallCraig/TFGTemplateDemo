DECLARE @SupportLoginName varchar(max)
DECLARE @QuoteSupportLoginName varchar(max)

DECLARE @ServiceAccount varchar(max)
DECLARE @QuoteServiceAccount varchar(max)

DECLARE @SQLName varchar(max)
DECLARE @QuoteSQLName varchar(max)
DECLARE @SQLPassword nvarchar(max)

DECLARE @QuoteDatabaseName varchar(max)
DECLARE @Schema varchar(max)
DECLARE @QuoteSchema varchar(max)
DECLARE @QuoteReconSchema varchar(max)

DECLARE @QuoteSupportRole varchar(max)
DECLARE @QuoteSystemRole varchar(max)

SET @SupportLoginName = 'HO\TFG Infotec Developers SG'
		
IF (@@servername LIKE '%DEV%' OR @@servername LIKE '%EXPRESS%' OR @@servername LIKE '%local%')
	BEGIN
		SELECT @@servername 
	
		SET @ServiceAccount = 'HO\svc_DevWebApp'

		SET @SQLName = 'svc_tfgtemplatedemo_dev'
		SET @SQLPassword = 'RedHat@OpenShift'
	END
ELSE IF (@@servername LIKE '%TST%')
	BEGIN
		SET @ServiceAccount = 'HO\svc_TstWebApp'

		SET @SQLName = 'svc_tfgtemplatedemo_tst'
		SET @SQLPassword = 'RedHat@OpenShift'
	END
ELSE IF (@@servername LIKE '%PRD%')
	BEGIN
		SET @ServiceAccount = 'HO\svc_PrdWebApp'

		SET @SQLName = 'svc_tfgtemplatedemo'
		SET @SQLPassword = 'RedHat@OpenShift'
	END
ELSE
BEGIN
	RETURN
END

SET @QuoteSupportLoginName = QUOTENAME(@SupportLoginName)
SET @QuoteSQLName = QUOTENAME(@SQLName)
SET @QuoteServiceAccount = QUOTENAME(@ServiceAccount)

SET @QuoteDatabaseName = DB_NAME()
SET @Schema = 'Tfg'
SET @QuoteSchema = QUOTENAME(@Schema)

SET @QuoteSupportRole = QUOTENAME('Application_Support')
SET @QuoteSystemRole = QUOTENAME('Application_System')

DECLARE @ServerEdition varchar(max)
SET @ServerEdition = (SELECT CAST(SERVERPROPERTY('Edition') AS varchar(max)))
IF (@ServerEdition NOT like '%Express%')
BEGIN
	-- Create login for support account
	IF NOT EXISTS (SELECT
			name
		FROM master.sys.server_principals
		WHERE name = @SupportLoginName)
	BEGIN
	EXECUTE ('CREATE LOGIN ' + @QuoteSupportLoginName + ' FROM WINDOWS WITH DEFAULT_DATABASE= ' + @QuoteDatabaseName + ', DEFAULT_LANGUAGE=[us_english]')
	END

	IF NOT EXISTS (SELECT
			name
		FROM sys.database_principals
		WHERE name = @SupportLoginName)
	BEGIN
	EXECUTE ('CREATE USER ' + @QuoteSupportLoginName + ' FOR LOGIN ' + @QuoteSupportLoginName + ' WITH DEFAULT_SCHEMA = ' + @QuoteSchema)
	END

	--Create login for service account
	IF NOT EXISTS (SELECT
			name
		FROM master.sys.server_principals
		WHERE name = @ServiceAccount)
	BEGIN
	EXECUTE ('CREATE LOGIN ' + @QuoteServiceAccount + ' FROM WINDOWS WITH DEFAULT_DATABASE= ' + @QuoteDatabaseName + ', DEFAULT_LANGUAGE=[us_english]')
	END

	IF NOT EXISTS (SELECT
			name
		FROM sys.database_principals
		WHERE name = @ServiceAccount)
	BEGIN
	EXECUTE ('CREATE USER ' + @QuoteServiceAccount + ' FOR LOGIN ' + @QuoteServiceAccount + ' WITH DEFAULT_SCHEMA = ' + @QuoteSchema)
	END

	--db roles
	IF NOT EXISTS (SELECT
			name
		FROM sys.database_principals
		WHERE type_desc = 'DATABASE_ROLE'
		AND name = 'Application_Support')
	BEGIN
		EXECUTE ('CREATE ROLE ' + @QuoteSupportRole)

		EXECUTE ('ALTER ROLE ' + @QuoteSupportRole + ' ADD MEMBER ' + @QuoteSupportLoginName)
	END

	IF NOT EXISTS (SELECT
			name
		FROM sys.database_principals
		WHERE type_desc = 'DATABASE_ROLE'
		AND name = 'Application_System')
	BEGIN
	EXECUTE ('CREATE ROLE ' + @QuoteSystemRole)

	EXECUTE ('ALTER ROLE ' + @QuoteSystemRole + ' ADD MEMBER ' + @QuoteServiceAccount)

	END

	EXECUTE ('GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON SCHEMA::' + @QuoteSchema + ' TO ' + @QuoteSystemRole)

	IF (@@servername like '%DEV%' OR @@servername LIKE '%EXPRESS%' OR @@servername LIKE '%local%')
		BEGIN
			EXECUTE ('GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON SCHEMA::' + @QuoteSchema + ' TO ' + @QuoteSupportRole)
			EXECUTE ('GRANT SELECT, EXECUTE ON SCHEMA::dbo TO ' + @QuoteSupportRole)
		END
	ELSE
		BEGIN
			EXECUTE ('GRANT SELECT, EXECUTE ON SCHEMA::' + @QuoteSchema + ' TO ' + @QuoteSupportRole)
			EXECUTE ('GRANT SELECT, EXECUTE ON SCHEMA::dbo TO ' + @QuoteSupportRole)
		END
END

-- Create SQL login for application
IF NOT EXISTS (SELECT name
	FROM master.sys.server_principals
	WHERE name = @SQLName)
BEGIN
	EXECUTE ('CREATE LOGIN ' + @QuoteSQLName + ' WITH PASSWORD=''' + @SQLPassword + ''', DEFAULT_DATABASE= ' + @QuoteDatabaseName + ', DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF')
END

IF NOT EXISTS (SELECT name
	FROM sys.database_principals
	WHERE name = @SQLName)
BEGIN
	EXECUTE ('CREATE USER ' + @QuoteSQLName + ' FOR LOGIN ' + @QuoteSQLName + ' WITH DEFAULT_SCHEMA = ' + @QuoteSchema)
	EXECUTE ('ALTER ROLE [db_owner] ADD MEMBER ' + @QuoteSQLName + '')
END

--db roles
IF NOT EXISTS (SELECT name
	FROM sys.database_principals
	WHERE type_desc = 'DATABASE_ROLE'
	AND name = 'Application_Support')
BEGIN
	EXECUTE ('CREATE ROLE ' + @QuoteSupportRole)
END

IF NOT EXISTS (SELECT name
	FROM sys.database_principals
	WHERE type_desc = 'DATABASE_ROLE'
	AND name = 'Application_System')
BEGIN
	EXECUTE ('CREATE ROLE ' + @QuoteSystemRole)
	EXECUTE ('ALTER ROLE ' + @QuoteSystemRole + ' ADD MEMBER ' + @QuoteSQLName)
END

EXECUTE ('GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON SCHEMA::' + @QuoteSchema + ' TO ' + @QuoteSystemRole)

IF (@@servername like '%DEV%' OR @@servername LIKE '%EXPRESS%' OR @@servername LIKE '%local%')
	BEGIN
		EXECUTE ('GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON SCHEMA::' + @QuoteSchema + ' TO ' + @QuoteSupportRole)
		EXECUTE ('GRANT SELECT, EXECUTE ON SCHEMA::dbo TO ' + @QuoteSupportRole)
	END
ELSE
	BEGIN
		EXECUTE ('GRANT SELECT, EXECUTE ON SCHEMA::' + @QuoteSchema + ' TO ' + @QuoteSupportRole)
		EXECUTE ('GRANT SELECT, EXECUTE ON SCHEMA::dbo TO ' + @QuoteSupportRole)
	END

GO