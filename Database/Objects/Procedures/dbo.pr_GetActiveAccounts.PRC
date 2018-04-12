
USE Bank

IF NOT EXISTS(SELECT * FROM sysobjects where id = object_id(N'dbo.pr_GetActiveAccounts') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  EXEC ('CREATE PROCEDURE dbo.pr_GetActiveAccounts AS SET NOCOUNT ON;')

GO

ALTER PROCEDURE dbo.pr_GetActiveAccounts

AS

  SELECT 
        ID
        ,Balance
        ,Currency
        ,AccountStatus
  FROM  tb_Accounts
  WHERE AccountStatus = 1;

GO
