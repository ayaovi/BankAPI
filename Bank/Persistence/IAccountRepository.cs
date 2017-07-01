using System.Collections.Generic;
using Bank.Models;

namespace Bank.Persistence
{
  public interface IAccountRepository
  {
    /// <summary>
    /// Gets the account.
    /// </summary>
    /// <param name="accountId">The account identifier.</param>
    /// <returns></returns>
    Account GetAccount(int accountId);

    /// <summary>
    /// Gets all accounts.
    /// </summary>
    /// <returns></returns>
    IList<Account> GetAllAccounts();

    /// <summary>
    /// Saves the specified account.
    /// </summary>
    /// <param name="account">The account.</param>
    void Save(Account account);
  }
}