
USE Bank

GO

IF NOT EXISTS(SELECT * FROM sysobjects where id = object_id(N'dbo.pr_GetExchangeRateFromOneCurrencyToAnother') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  EXEC ('CREATE PROCEDURE dbo.pr_GetExchangeRateFromOneCurrencyToAnother AS SET NOCOUNT ON;')

GO

ALTER PROCEDURE dbo.pr_GetExchangeRateFromOneCurrencyToAnother
  @OriginCurrencyID INT = NULL
  ,@DestinationCurrencyID INT = NULL
AS

  SELECT (SELECT Value FROM tb_Currencies WHERE ID = @OriginCurrencyID) / (SELECT Value FROM tb_Currencies WHERE ID = @DestinationCurrencyID)
  
GO
