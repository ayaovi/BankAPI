USE Bank

GO

IF NOT EXISTS(SELECT * FROM sysobjects where id = object_id(N'dbo.pr_CloseAccounts') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  EXEC ('CREATE PROCEDURE dbo.pr_CloseAccounts AS SET NOCOUNT ON;')

GO

ALTER PROCEDURE dbo.pr_CloseAccounts
  @AccountIDs dbo.tp_IntTable READONLY
AS 

  UPDATE  tb_Accounts
  SET     AccountStatus = 0
    WHERE AccountID IN (SELECT * FROM @AccountIDs);

GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.pr_CloseAccounts.PRC', @Version = 1, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.pr_CloseAccounts'''
END

