using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bank.Persistence.Common;
using Dapper;

namespace Bank.Persistence.Account
{
  public class AccountRepository : IAccountRepository
  {
    /// <summary>
    /// The connection string provider
    /// </summary>
    private readonly IConnectionStringProvider _connectionStringProvider;

    /// <summary>
    /// The proc for getting active accounts.
    /// </summary>
    private const string ProcGetActiveAccounts = "dbo.pr_GetActiveAccounts";

    /// <summary>
    /// The proc get account by account ids
    /// </summary>
    private const string ProcGetAccountByAccountIDs = "dbo.pr_GetAccountByAccountIDs";

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountRepository"/> class.
    /// </summary>
    public AccountRepository(IConnectionStringProvider connectionStringProvider)
    {
      _connectionStringProvider = connectionStringProvider;
    }

    /// <summary>
    /// Gets all accounts asynchronously.
    /// </summary>
    /// <param name="accountIds"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Models.Account>> GetAccountsAsync(IEnumerable<int> accountIds)
    {
      using (var connection = new SqlConnection(await _connectionStringProvider.GetConnectionStringAsync()))
      {
        await connection.OpenAsync();
        var accountIdsDataTable = Utilities.Util.ToIntTable(accountIds);
        var parameters = new
        {
          AccountIDs = accountIdsDataTable
        };
        return await connection.QueryAsync<Models.Account>(ProcGetAccountByAccountIDs, parameters, commandType: CommandType.StoredProcedure);
      }
    }
  }
}