using System.Collections.Generic;
using System.Linq;
using Bank.Models;
using NUnit.Framework;
using Bank.Persistence;

namespace Bank.Tests.Persistence
{
  [TestFixture]
  internal class AccountRepositoryTests
  {
    [Test]
    public void GetAccount_GivenRepository_ExpectSuccess()
    {
      //Arrange
      var repository = new AccountRepository();
      var account = new Account { Id = 3097, Balance = 352 };

      //Act
      repository.Save(new Account { Id = 1786, Balance = 1000, Currency = Currency.Usd });
      repository.Save(new Account { Id = 2864, Balance = 500, Currency = Currency.Euro });
      repository.Save(new Account { Id = 3097, Balance = 352 });

      //Assert
      Assert.IsTrue(Equals(repository.GetAccount(3097), account));
    }

    [Test]
    public void GetAllAccounts_GivenRepository_ExpectSuccess()
    {
      //Arrange
      var repository = new AccountRepository();
      var expected = new List<Account>
      {
        new Account { Id = 3097, Balance = 352 },
        new Account { Id = 1786, Balance = 1000, Currency = Currency.Usd },
        new Account { Id = 2864, Balance = 500, Currency = Currency.Euro }
      };

      //Act
      repository.Save(new Account { Id = 1786, Balance = 1000, Currency = Currency.Usd });
      repository.Save(new Account { Id = 2864, Balance = 500, Currency = Currency.Euro });
      repository.Save(new Account { Id = 3097, Balance = 352 });

      var accounts = repository.GetAllAccounts();

      //Assert
      Assert.IsTrue(expected.All(account => accounts.Contains(account)));
    }
  }
}