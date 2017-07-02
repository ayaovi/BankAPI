
USE Bank

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.types WHERE name = 'tp_TransactionTable')
BEGIN
  CREATE TYPE dbo.tp_TransactionTable AS TABLE
  (
    AccountID INT NOT NULL
    ,TransactionDate DATETIME NOT NULL
    ,Amount MONEY NOT NULL
  )
END

GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.tp_TransactionTable.TYP', @Version = 1, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.tp_TransactionTable'''
END

