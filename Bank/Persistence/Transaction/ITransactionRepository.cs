using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Persistence.Transaction
{
  public interface ITransactionRepository
  {
    /// <summary>
    /// Gets the transaction by account ids asynchronously.
    /// </summary>
    /// <param name="accountIds">The account ids.</param>
    /// <returns></returns>
    Task<IEnumerable<Models.Transaction>> GetTransactionByAccountIdsAsync(IEnumerable<int> accountIds);

    /// <summary>
    /// Gets the transactions asynchronously.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Models.Transaction>> GetTransactionsAsync();
  }
}