using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Bank.Models;

namespace Bank.Persistence
{
  public class AccountRepository : IAccountRepository
  {
    /// <summary>
    /// The accounts
    /// </summary>
    private static readonly Lazy<ConcurrentDictionary<int, Account>> Accounts = new Lazy<ConcurrentDictionary<int, Account>>(InitialiseDictionary, true);

    /// <summary>
    /// Initialises the account dictionary.
    /// </summary>
    /// <returns></returns>
    private static ConcurrentDictionary<int, Account> InitialiseDictionary()
    {
      return new ConcurrentDictionary<int, Account>();
    }

    /// <summary>
    /// Gets the account corresponding to the supplied accountId.
    /// </summary>
    /// <param name="accountId">The account identifier.</param>
    /// <returns>
    /// Account if it exists or null otherwise.
    /// </returns>
    public Account GetAccount(int accountId)
    {
      Account account;
      var found = Accounts.Value.TryGetValue(accountId, out account);
      return found ? account : null;
    }

    /// <summary>
    /// Gets all accounts.
    /// </summary>
    /// <returns></returns>
    public IList<Account> GetAllAccounts()
    {
      return Accounts.Value.Values.ToList();
    }

    /// <summary>
    /// Saves the specified account.
    /// </summary>
    /// <param name="account">The account.</param>
    public void Save(Account account)
    {
      Accounts.Value.AddOrUpdate(account.Id, account, (i, oldAccount) => account);
    }
  }
}