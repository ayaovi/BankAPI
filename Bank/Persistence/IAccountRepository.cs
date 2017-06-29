using System.Collections.Generic;
using System.Drawing;
using Bank.Models;

namespace Bank.Persistence
{
  public interface IAccountRepository
  {
    Account GetAccount(int accountId);
    IList<Account> GetAllAccounts();
    void Save(Account account);
  }
}