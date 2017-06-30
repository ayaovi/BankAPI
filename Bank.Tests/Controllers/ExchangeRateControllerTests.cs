using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Bank.Controllers;
using Bank.Models;
using NUnit.Framework;

namespace Bank.Tests.Controllers
{
  [TestFixture]
  internal class ExchangeRateControllerTests
  {
    [Test]
    public void Post_GivenRepository_ExpectSuccess()
    {
      //Arrange
      var controller = new ExchangeRateController();

      //Act
      var actionResult = controller.Post(new Dictionary<Currency, decimal>
      {
        {Currency.Rand, 1},
        {Currency.Usd, (decimal)12.96},
        {Currency.Euro, (decimal)14.80}
      });

      //Assert
      Assert.IsInstanceOf<OkResult>(actionResult);
    }

    [TestCase(Currency.Rand, Currency.Rand, 1, TestName = "Rand_to_Rand")]
    [TestCase(Currency.Rand, Currency.Usd, 0.0771604938271604938271604938, TestName = "Rand_to_Usd")]
    [TestCase(Currency.Rand, Currency.Euro, 0.0675675675675675675675675676, TestName = "Rand_to_Euro")]
    [TestCase(Currency.Usd, Currency.Rand, 12.96, TestName = "Usd_to_Rand")]
    [TestCase(Currency.Usd, Currency.Usd, 1, TestName = "Usd_to_Usd")]
    [TestCase(Currency.Usd, Currency.Euro, 0.8756756756756756756756756757, TestName = "Usd_to_Euro")]
    [TestCase(Currency.Euro, Currency.Rand, 14.8, TestName = "Euro_to_Rand")]
    [TestCase(Currency.Euro, Currency.Usd, 1.1419753086419753086419753086, TestName = "Euro_to_Usd")]
    [TestCase(Currency.Euro, Currency.Euro, 1, TestName = "Euro_to_Euro")]
    public void Get_GivenExchangeRepository_ExpectSuccess(Currency from, Currency to, decimal rate)
    {
      //Arrange
      var controller = new ExchangeRateController
      {
        Request = new HttpRequestMessage(),
        Configuration = new HttpConfiguration()
      };

      //Act
      controller.Post(new Dictionary<Currency, decimal>
      {
        {Currency.Rand, 1},
        {Currency.Usd, (decimal)12.96},
        {Currency.Euro, (decimal)14.80}
      });
      var response = controller.Get(from, to);

      //Assert
      Assert.IsInstanceOf<OkNegotiatedContentResult<decimal>>(response);
      Assert.IsTrue(decimal.Round(((OkNegotiatedContentResult<decimal>)response).Content, 14) == decimal.Round(rate, 14));
    }
  }
}