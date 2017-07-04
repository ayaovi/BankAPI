
USE Bank

GO

IF NOT EXISTS(SELECT * FROM sysobjects where id = object_id(N'dbo.pr_GetTransactionByAccountIDs') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  EXEC ('CREATE PROCEDURE dbo.pr_GetTransactionByAccountIDs AS SET NOCOUNT ON;')

GO

ALTER PROCEDURE dbo.pr_GetTransactionByAccountIDs
  @AccountIDs tp_IntTable READONLY

AS

  SELECT 
        AccountID 
        ,TransactionDate as Date
        ,Amount 
  FROM  tb_Transactions
  WHERE AccountID IN (SELECT Value FROM @AccountIDs);

GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.pr_GetTransactionByAccountIDs.PRC', @Version = 1, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.pr_GetTransactionByAccountIDs'''
END

GO

