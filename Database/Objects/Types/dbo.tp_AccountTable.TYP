USE Bank

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.types WHERE name = 'tp_AccountTable')
BEGIN
  CREATE TYPE dbo.tp_AccountTable AS TABLE
  (
    AccountID INT NOT NULL
    ,Balance MONEY NOT NULL
    ,Currency VARCHAR(50) NOT NULL
    ,AccountStatus BIT NOT NULL 
  )
END
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.tp_AccountTable.TYP', @Version = 1, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.tp_AccountTable'''
END