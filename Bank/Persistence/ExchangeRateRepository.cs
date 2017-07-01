using System;
using System.Collections.Concurrent;
using Bank.Models;

namespace Bank.Persistence
{
  public class ExchangeRateRepository : IExchangeRateRepository
  {
    /// <summary>
    /// The rates
    /// </summary>
    private static readonly Lazy<ConcurrentDictionary<Currency, decimal>> Rates = new Lazy<ConcurrentDictionary<Currency, decimal>>(InitialiseRates, true);

    /// <summary>
    /// Initialises the rates.
    /// </summary>
    /// <returns></returns>
    private static ConcurrentDictionary<Currency, decimal> InitialiseRates()
    {
      return new ConcurrentDictionary<Currency, decimal>();
    }

    /// <summary>
    /// Gets the exchange rate.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    /// <returns></returns>
    public decimal GetExchangeRate(Currency from, Currency to) => Rates.Value[from] / Rates.Value[to];

    /// <summary>
    /// Saves the specified currency.
    /// </summary>
    /// <param name="currency">The currency.</param>
    /// <param name="rate">The rate.</param>
    public void Save(Currency currency, decimal rate)
    {
      Rates.Value.TryAdd(currency, rate);
    }
  }
}