USE Bank

GO

IF NOT EXISTS(SELECT * FROM sysobjects where id = object_id(N'dbo.pr_CreateAccounts') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  EXEC ('CREATE PROCEDURE dbo.pr_CreateAccounts AS SET NOCOUNT ON;')

GO

ALTER PROCEDURE dbo.pr_CreateAccounts
  @Accounts dbo.tp_AccountTable READONLY
AS 
  CREATE TABLE #tmpAccounts (
    AccountID INT NOT NULL
    ,Balance MONEY NOT NULL
    ,Currency VARCHAR(50) NOT NULL
    ,AccountStatus BIT NOT NULL 
  );

  CREATE TABLE #tmpAccountErrors (
    AccountID INT NOT NULL
    ,Balance MONEY NOT NULL
    ,Currency VARCHAR(50) NOT NULL
    ,AccountStatus BIT NOT NULL 
    ,ErrorMessage VARCHAR(256) NOT NULL
  );

  INSERT  #tmpAccounts (
             AccountID
            ,Balance
            ,Currency
            ,AccountStatus
          )
  SELECT  DISTINCT 
          AccountID
          ,Balance
          ,Currency
          ,AccountStatus
  FROM    @Accounts;

  DELETE  src
  OUTPUT  DELETED.AccountID
          ,DELETED.Balance
          ,DELETED.Currency
          ,DELETED.AccountStatus
          ,'Duplicate AccountID for different accounts.'
  INTO    #tmpAccountErrors(
             AccountID
            ,Balance
            ,Currency
            ,AccountStatus
            ,ErrorMessage
          )
  FROM    #tmpAccounts src 
  WHERE   src.AccountID IN (
            SELECT AccountID 
            FROM @Accounts 
            GROUP BY AccountID 
            HAVING Count(AccountID) > 1
          );

  DELETE  src
  OUTPUT  DELETED.AccountID
          ,DELETED.Balance
          ,DELETED.Currency
          ,DELETED.AccountStatus
          ,'NULL value specified for Balance.'
  INTO    #tmpAccountErrors(
             AccountID
            ,Balance
            ,Currency
            ,AccountStatus
            ,ErrorMessage
          )
  FROM    #tmpAccounts src 
  WHERE   src.Balance IS NULL;

  DELETE  src
  OUTPUT  DELETED.AccountID
          ,DELETED.Balance
          ,DELETED.Currency
          ,DELETED.AccountStatus
          ,'NULL value specified for Currency.'
  INTO    #tmpAccountErrors(
             AccountID
            ,Balance
            ,Currency
            ,AccountStatus
            ,ErrorMessage
          )
  FROM    #tmpAccounts src 
  WHERE   src.Currency IS NULL;

  DELETE  src
  OUTPUT  DELETED.AccountID
          ,DELETED.Balance
          ,DELETED.Currency
          ,DELETED.AccountStatus
          ,'NULL value specified for AccountStatus.'
  INTO    #tmpAccountErrors(
             AccountID
            ,Balance
            ,Currency
            ,AccountStatus
            ,ErrorMessage
          )
  FROM    #tmpAccounts src 
  WHERE   src.AccountStatus IS NULL;

  MERGE   dbo.tb_Accounts dst
  USING   #tmpAccounts src
          ON src.AccountID = dst.AccountID
  --WHEN    MATCHED AND src.Balance IS NOT NULL AND src.Currency IS NOT NULL THEN UPDATE 
  --        SET dst.Balance = src.Balance;
  --        SET dst.Currency = src.Currency
  --WHEN    MATCHED AND src.Currency IS NOT NULL THEN UPDATE 
  --        SET dst.Currency = src.Currency
  WHEN    NOT MATCHED THEN INSERT (
            AccountID
            ,Balance
            ,Currency
            ,AccountStatus
          )
          VALUES (
            src.AccountID
            ,src.Balance
            ,src.Currency
            ,src.AccountStatus
          );

  SELECT  AccountID
          ,Balance
          ,Currency
          ,AccountStatus
          ,ErrorMessage
  FROM    #tmpAccountErrors;

GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.pr_CreateAccounts.PRC', @Version = 1, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.pr_CreateAccounts'''
END

