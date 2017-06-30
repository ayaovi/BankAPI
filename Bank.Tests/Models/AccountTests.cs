using Bank.Models;
using NUnit.Framework;

namespace Bank.Tests.Models
{
  [TestFixture]
  internal class AccountTests
  {
    [Test]
    public void Equals_GivenAccount_ExpectResult()
    {
      //Assert
      Assert.IsTrue(Equals(new Account { Id = 3097, Balance = 352 }, new Account { Id = 3097, Balance = 352 }));
      Assert.IsFalse(Equals(new Account { Id = 3097, Balance = 352 }, new Account { Id = 3097, Balance = 352, Currency = Currency.Euro}));
    }
  }
}