using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Persistence.Currency
{
  public interface IExchangeRateRepository
  {
    /// <summary>
    /// Gets the exchange rate.
    /// </summary>
    /// <param name="originCurrency"></param>
    /// <param name="destinationCurrency"></param>
    /// <returns></returns>
    Task<IEnumerable<decimal>> GetExchangeRateAsync(Models.Currency originCurrency, Models.Currency destinationCurrency);

    //void Save(Models.Currency currency, decimal rate);
  }
}