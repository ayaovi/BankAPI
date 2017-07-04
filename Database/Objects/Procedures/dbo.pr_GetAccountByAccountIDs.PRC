
USE Bank

IF NOT EXISTS(SELECT * FROM sysobjects where id = object_id(N'dbo.pr_GetAccountByAccountIDs') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  EXEC ('CREATE PROCEDURE dbo.pr_GetAccountByAccountIDs AS SET NOCOUNT ON;')

GO

ALTER PROCEDURE dbo.pr_GetAccountByAccountIDs
  @AccountIDs tp_IntTable READONLY
AS

  SELECT 
        ID
        ,Balance
        ,Currency
        ,AccountStatus as Status
  FROM  tb_Accounts
  WHERE ID IN (SELECT Value FROM @AccountIDs);

GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.pr_GetAccountByAccountIDs.PRC', @Version = 1, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.pr_GetAccountByAccountIDs'''
END

GO

