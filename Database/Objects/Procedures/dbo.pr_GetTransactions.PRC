
USE Bank

GO

IF NOT EXISTS(SELECT * FROM sysobjects where id = object_id(N'dbo.pr_GetTransactions') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  EXEC ('CREATE PROCEDURE dbo.pr_GetTransactions AS SET NOCOUNT ON;')

GO

ALTER PROCEDURE dbo.pr_GetTransactions

AS

  SELECT 
        AccountID 
        ,TransactionDate 
        ,Amount 
  FROM  tb_Transactions;

GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.pr_GetTransactions.PRC', @Version = 1, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.pr_GetTransactions'''
END

GO

