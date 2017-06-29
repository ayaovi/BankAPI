using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Bank.Models;

namespace Bank.Persistence
{
  public class AccountRepository : IAccountRepository
  {
    private static readonly Lazy<ConcurrentDictionary<int, Account>> Accounts = new Lazy<ConcurrentDictionary<int, Account>>(InitialiseDictionary, true);

    private static ConcurrentDictionary<int, Account> InitialiseDictionary()
    {
      return new ConcurrentDictionary<int, Account>();
    }

    public Account GetAccount(int accountId)
    {
      Account account;
      var found = Accounts.Value.TryGetValue(accountId, out account);
      return found ? account : null;
    }

    public IList<Account> GetAllAccounts()
    {
      return Accounts.Value.Values.ToList();
    }

    public void Save(Account account)
    {
      Accounts.Value.AddOrUpdate(account.Id, account, (i, oldAccount) => account);
    }
  }
}