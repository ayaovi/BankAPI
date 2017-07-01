using System.Collections.Generic;
using System.Web.Http.Results;
using Bank.Controllers;
using Bank.Models;
using NUnit.Framework;

namespace Bank.Tests.Controllers
{
  [TestFixture]
  internal class DepositControllerTests
  {
    [Test]
    public void Post_GivenDepositAndExistingAccount_ExpectSuccess()
    {
      //Arrange
      new AccountController().Post(new List<Account>
      {
        new Account
        {
          Id = 1002547,
          Balance = 25
        }
      });

      new ExchangeRateController().Post(new Dictionary<Currency, decimal>
      {
        {Currency.Rand, 1},
        {Currency.Usd, (decimal)12.96},
        {Currency.Euro, (decimal)14.80}
      });

      var controller = new DepositController();

      //Act
      var actionResult = controller.Post(new Deposit { AccountId = 1002547, Amount = 100, Currency = Currency.Euro });
      var result = new AccountController().Get(1002547);

      //Assert
      Assert.IsInstanceOf<OkNegotiatedContentResult<Account>>(actionResult);
      Assert.IsTrue(((OkNegotiatedContentResult<Account>)result).Content.Balance == 1505);
    }
  }
}