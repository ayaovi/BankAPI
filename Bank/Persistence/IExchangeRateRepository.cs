using Bank.Models;

namespace Bank.Persistence
{
  public interface IExchangeRateRepository
  {
    /// <summary>
    /// Gets the exchange rate.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    /// <returns></returns>
    decimal GetExchangeRate(Currency from, Currency to);

    /// <summary>
    /// Saves the specified currency.
    /// </summary>
    /// <param name="currency">The currency.</param>
    /// <param name="rate">The rate.</param>
    void Save(Currency currency, decimal rate);
  }
}