
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
    WHERE ID IN (SELECT * FROM @AccountIDs);

GO
