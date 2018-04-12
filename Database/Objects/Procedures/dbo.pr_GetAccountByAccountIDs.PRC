
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
