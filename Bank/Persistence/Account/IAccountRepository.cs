using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Persistence.Account
{
  public interface IAccountRepository
  {
    /// <summary>
    /// Gets all accounts asynchronously.
    /// </summary>
    /// <param name="accountIds"></param>
    /// <returns></returns>
    Task<IEnumerable<Models.Account>> GetAccountsAsync(IEnumerable<int> accountIds);
  }
}