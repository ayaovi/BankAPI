
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
