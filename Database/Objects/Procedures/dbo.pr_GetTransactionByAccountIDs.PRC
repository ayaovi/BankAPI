
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
