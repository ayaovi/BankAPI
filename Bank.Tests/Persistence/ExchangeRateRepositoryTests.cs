using Bank.Models;
using Bank.Persistence;
using NUnit.Framework;

namespace Bank.Tests.Persistence
{
  [TestFixture]
  internal class ExchangeRateRepositoryTests
  {
    [TestCase(Currency.Rand, Currency.Rand, 1, TestName = "Rand_to_Rand")]
    [TestCase(Currency.Rand, Currency.Usd, 0.0771604938271604938271604938, TestName = "Rand_to_Usd")]
    [TestCase(Currency.Rand, Currency.Euro, 0.0675675675675675675675675676, TestName = "Rand_to_Euro")]
    [TestCase(Currency.Usd, Currency.Rand, 12.96, TestName = "Usd_to_Rand")]
    [TestCase(Currency.Usd, Currency.Usd, 1, TestName = "Usd_to_Usd")]
    [TestCase(Currency.Usd, Currency.Euro, 0.8756756756756756756756756757, TestName = "Usd_to_Euro")]
    [TestCase(Currency.Euro, Currency.Rand, 14.8, TestName = "Euro_to_Rand")]
    [TestCase(Currency.Euro, Currency.Usd, 1.1419753086419753086419753086, TestName = "Euro_to_Usd")]
    [TestCase(Currency.Euro, Currency.Euro, 1, TestName = "Euro_to_Euro")]
    public void GetExchangeRate_GivenRepository_ExpectSuccess(Currency from, Currency to, decimal rate)
    {
      //Arrange
      var repository = new ExchangeRateRepository();

      //Act
      repository.Save(Currency.Rand, 1);
      repository.Save(Currency.Usd, (decimal) 12.96);
      repository.Save(Currency.Euro, (decimal) 14.80);

      //Assert
      Assert.AreEqual(decimal.Round(rate, 14), decimal.Round(repository.GetExchangeRate(from, to), 14));
    }
  }
}
