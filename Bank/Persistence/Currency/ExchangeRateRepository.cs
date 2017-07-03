using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bank.Persistence.Common;
using Dapper;

namespace Bank.Persistence.Currency
{
  public class ExchangeRateRepository : IExchangeRateRepository
  {
    /// <summary>
    /// The connection factory
    /// </summary>
    private readonly IConnectionStringProvider _connectionStringProvider;

    /// <summary>
    /// The proc get exchange rate
    /// </summary>
    private const string ProcGetExchangeRate = "dbo.pr_GetExchangeRateFromOneCurrencyToAnother";

    /// <summary>
    /// Initializes a new instance of the <see cref="ExchangeRateRepository"/> class.
    /// </summary>
    /// <param name="connectionStringProvider"></param>
    public ExchangeRateRepository(IConnectionStringProvider connectionStringProvider)
    {
      _connectionStringProvider = connectionStringProvider;
    }

    /// <summary>
    /// Gets the exchange rate.
    /// </summary>
    /// <param name="originCurrency"></param>
    /// <param name="destinationCurrency"></param>
    /// <returns></returns>
    public async Task<IEnumerable<decimal>> GetExchangeRateAsync(Models.Currency originCurrency, Models.Currency destinationCurrency)
    {
      using (var connection = new SqlConnection(await _connectionStringProvider.GetConnectionStringAsync()))
      {
        await connection.OpenAsync();
        var parameters = new
        {
          OriginCurrencyID = (int) originCurrency,
          DestinationCurrencyID = (int) destinationCurrency
        };
        return await connection.QueryAsync<decimal>(ProcGetExchangeRate, parameters, commandType: CommandType.StoredProcedure);
      }
    }
  }
}