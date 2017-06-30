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
  internal class AccountControllerTests
  {
    [Test]
    public void Post_GivenAccountRepository_ExpectSuccess()
    {
      //Arrange
      var controller = new AccountController();

      //Act
      var actionResult = controller.Post(new List<Account>
      {
        new Account
        {
          Id = 1786,
          Balance = 1000,
          Currency = Currency.Usd
        },
        new Account
        {
          Id = 2864,
          Balance = 500,
          Currency = Currency.Euro
        },
        new Account
        {
          Id = 3097,
          Balance = 352
        },
        new Account
        {
          Id = 4712,
          Balance = 400
        },
        new Account
        {
          Id = 6768,
          Balance = 50000
        }
      });

      //Assert
      Assert.IsInstanceOf<OkResult>(actionResult);
    }

    [Test]
    public void Get_GivenAccountRepository_ExpectSuccess()
    {
      //Arrange
      var controller = new AccountController
      {
        Request = new HttpRequestMessage(),
        Configuration = new HttpConfiguration()
      };
      var expectedAccount = new Account
      {
        Id = 13548,
        Balance = 55367
      };

      //Act
      controller.Post(new List<Account>
      {
        new Account
        {
          Id = 13548,
          Balance = 55367
        }
      });
      var response = controller.Get(13548);

      //Assert
      Assert.IsInstanceOf<OkNegotiatedContentResult<Account>>(response);
      Assert.IsTrue(Equals(expectedAccount, ((OkNegotiatedContentResult<Account>)response).Content));
    }
  }
}