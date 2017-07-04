
USE Bank

GO

IF NOT EXISTS(SELECT * FROM sysobjects where id = object_id(N'dbo.pr_MakeDeposit') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  EXEC ('CREATE PROCEDURE dbo.pr_MakeDeposit AS SET NOCOUNT ON;')

GO

ALTER PROCEDURE dbo.pr_MakeDeposit
  @AccountID INT = NULL,
  @Amount MONEY = NULL

AS 
BEGIN

  DECLARE @NewBalance MONEY
  SELECT @NewBalance = (SELECT Balance FROM tb_Accounts WHERE AccountID = @AccountID) + @Amount 
  
  UPDATE dbo.tb_Accounts
  SET Balance = @NewBalance
  WHERE AccountID = @AccountID;

  --DECLARE @Transaction AS dbo.tp_TransactionTable
  INSERT INTO dbo.tb_Transactions (AccountID, TransactionDate, Amount) VALUES (@AccountID, CURRENT_TIMESTAMP, @Amount)

END

GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.pr_MakeDeposit.PRC', @Version = 2, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.pr_MakeDeposit'''
END

GO

