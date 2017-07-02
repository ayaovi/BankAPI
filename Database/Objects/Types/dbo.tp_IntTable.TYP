
USE Bank

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.types WHERE name = 'tp_IntTable')
BEGIN
  CREATE TYPE dbo.tp_IntTable AS TABLE
  (
    Value INT NULL
  )
END
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.tp_IntTable.TYP', @Version = 1, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.tp_IntTable'''
END