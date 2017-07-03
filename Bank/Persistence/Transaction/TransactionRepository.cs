using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bank.Persistence.Common;
using Dapper;

namespace Bank.Persistence.Transaction
{
  public class TransactionRepository : ITransactionRepository
  {

    private readonly IConnectionStringProvider _connectionStringProvider;

    /// <summary>
    /// The proc for getting transaction by account ids.
    /// </summary>
    private const string ProcGetTransactionByAccountIds = "dbo.pr_GetTransactionByAccountIds";

    /// <summary>
    /// The proc for getting all transactions.
    /// </summary>
    private const string ProcGetTransactions = "dbo.pr_GetTransactions";

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
    /// </summary>
    /// <param name="connectionStringProvider"></param>
    public TransactionRepository(IConnectionStringProvider connectionStringProvider)
    {
      _connectionStringProvider = connectionStringProvider;
    }

    /// <summary>
    /// Gets the transaction by account ids asynchronously.
    /// </summary>
    /// <param name="accountIds">The account ids.</param>
    /// <returns></returns>
    public async Task<IEnumerable<Models.Transaction>> GetTransactionByAccountIdsAsync(IEnumerable<int> accountIds)
    {
      using (var connection = new SqlConnection(await _connectionStringProvider.GetConnectionStringAsync()))
      {
        await connection.OpenAsync();
        var accountIdsDataTable = Utilities.Util.ToIntTable(accountIds);
        var parameters = new 
        {
          AccountIDs = accountIdsDataTable
        };
        return await connection.QueryAsync<Models.Transaction>(ProcGetTransactionByAccountIds, param: parameters, commandType: CommandType.StoredProcedure);
      }
    }

    /// <summary>
    /// Gets the transactions asynchronously.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Models.Transaction>> GetTransactionsAsync()
    {
      using (var connection = new SqlConnection(await _connectionStringProvider.GetConnectionStringAsync()))
      {
        await connection.OpenAsync();
        return await connection.QueryAsync<Models.Transaction>(ProcGetTransactions, commandType: CommandType.StoredProcedure);
      }
    }
  }
}