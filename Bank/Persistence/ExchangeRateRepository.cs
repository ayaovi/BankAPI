using System;
using System.Collections.Concurrent;
using Bank.Models;

namespace Bank.Persistence
{
  public class ExchangeRateRepository : IExchangeRateRepository
  {
    private static readonly Lazy<ConcurrentDictionary<Currency, decimal>> Rates = new Lazy<ConcurrentDictionary<Currency, decimal>>(InitialiseRates, true);

    private static ConcurrentDictionary<Currency, decimal> InitialiseRates()
    {
      return new ConcurrentDictionary<Currency, decimal>();
    }

    public decimal GetExchangeRate(Currency @from, Currency to) => Rates.Value[from] / Rates.Value[to];
    public void Save(Currency currency, decimal rate)
    {
      Rates.Value.TryAdd(currency, rate);
    }
  }
}