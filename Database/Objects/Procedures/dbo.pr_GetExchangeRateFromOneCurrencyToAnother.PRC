
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

IF EXISTS(SELECT 1 FROM sys.procedures WHERE name = 'sp_Version')
BEGIN
  EXEC dbo.sp_Version @dboObject = 'dbo.pr_GetExchangeRateFromOneCurrencyToAnother.PRC', @Version = 1, @ExpectedHash = '03E3964FA4838CFCD3F8B6F10FE520A2'
END
ELSE
BEGIN
  PRINT 'Could not version ''dbo.pr_GetExchangeRateFromOneCurrencyToAnother'''
END

GO

