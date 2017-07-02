USE Bank

IF NOT EXISTS(SELECT * FROM sysobjects where id = object_id(N'dbo.pr_GetActiveAccounts') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  EXEC ('CREATE PROCEDURE dbo.pr_GetActiveAccounts AS SET NOCOUNT ON;')

GO

ALTER PROCEDURE dbo.pr_GetActiveAccounts

AS

  SELECT 
        AccountID
        ,Balance
        ,Currency
        ,AccountStatus
  FROM  tb_Accounts
  WHERE AccountStatus = 1;

GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.pr_GetActiveAccounts.PRC', @Version = 2, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.pr_GetActiveAccounts'''
END

