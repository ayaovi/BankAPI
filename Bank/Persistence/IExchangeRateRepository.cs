using Bank.Models;

namespace Bank.Persistence
{
  public interface IExchangeRateRepository
  {
    decimal GetExchangeRate(Currency from, Currency to);
    void Save(Currency currency, decimal rate);
  }
}