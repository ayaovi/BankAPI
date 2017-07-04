
@echo off
del /s /q TablesAndObjects.sql

echo Building script...

echo  Tables

echo  Tables\dbo.tb_Accounts.TAB
type  Tables\dbo.tb_Accounts.TAB >> TablesAndObjects.sql

echo  Tables\dbo.tb_Currencies.TAB
type  Tables\dbo.tb_Currencies.TAB >> TablesAndObjects.sql

echo  Tables\dbo.tb_Transactions.TAB
type  Tables\dbo.tb_Transactions.TAB >> TablesAndObjects.sql

echo  Types

echo  Types\dbo.tp_AccountsTable.TYP
type  Types\dbo.tp_AccountsTable.TYP >> TablesAndObjects.sql

echo  Types\dbo.tp_IntTable.TYP
type  Types\dbo.tp_IntTable.TYP >> TablesAndObjects.sql

echo  Types\dbo.tp_TransactionTable.TYP
type  Types\dbo.tp_TransactionTable.TYP >> TablesAndObjects.sql

echo  Procedures

echo  Procedures\dbo.pr_CreateAccounts.PRC
type  Procedures\dbo.pr_CreateAccounts.PRC >> TablesAndObjects.sql

echo  Procedures\dbo.pr_CloseAccounts.PRC
type  Procedures\dbo.pr_CloseAccounts.PRC >> TablesAndObjects.sql

echo  Procedures\dbo.pr_GetAccountByAccountIDs.PRC
type  Procedures\dbo.pr_GetAccountByAccountIDs.PRC >> TablesAndObjects.sql

echo  Procedures\dbo.pr_GetActiveAccounts.PRC
type  Procedures\dbo.pr_GetActiveAccounts.PRC >> TablesAndObjects.sql

echo  Procedures\dbo.pr_GetExchangeRateFromOneCurrencyToAnother.PRC
type  Procedures\dbo.pr_GetExchangeRateFromOneCurrencyToAnother.PRC >> TablesAndObjects.sql

echo  Procedures\dbo.pr_GetTransactionByAccountIDs.PRC
type  Procedures\dbo.pr_GetTransactionByAccountIDs.PRC >> TablesAndObjects.sql

echo  Procedures\dbo.pr_GetTransactions.PRC
type  Procedures\dbo.pr_GetTransactions.PRC >> TablesAndObjects.sql

echo  Procedures\dbo.pr_MakeDeposit.PRC
type  Procedures\dbo.pr_MakeDeposit.PRC >> TablesAndObjects.sql

echo Done...

PAUSE
