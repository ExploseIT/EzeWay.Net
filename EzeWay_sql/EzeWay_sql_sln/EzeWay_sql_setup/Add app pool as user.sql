USE ezeway_db;
GO

-- Check if the login exists at the server level
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'IIS APPPOOL\ezeway AppPool')
BEGIN
    PRINT 'Creating SQL Server login for IIS APPPOOL\ezeway AppPool...';
    CREATE LOGIN [IIS APPPOOL\ezeway AppPool] FROM WINDOWS;
END
ELSE
BEGIN
    PRINT 'Login already exists.';
END
GO

-- Check if the user exists in the current database
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'IIS APPPOOL\ezeway AppPool')
BEGIN
    PRINT 'Creating database user for IIS APPPOOL\ezeway AppPool...';
    CREATE USER [IIS APPPOOL\ezeway AppPool] FOR LOGIN [IIS APPPOOL\ezeway AppPool];
    ALTER ROLE db_owner ADD MEMBER [IIS APPPOOL\ezeway AppPool];
END
ELSE
BEGIN
    PRINT 'Database user already exists.';
END
GO
