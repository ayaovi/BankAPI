using System.Configuration;
using System.Threading.Tasks;

namespace Bank.Persistence.Common
{
  public class ConnectionStringProvider : IConnectionStringProvider
  {
    /// <summary>
    /// The connection string
    /// </summary>
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectionStringProvider"/> class.
    /// </summary>
    public ConnectionStringProvider()
    {
      _connectionString = ConfigurationManager.ConnectionStrings["BankDBConnectionString"].ToString();
    }

    /// <summary>
    /// Gets the connection string asynchronously.
    /// </summary>
    /// <returns></returns>
    public Task<string> GetConnectionStringAsync() => Task.FromResult(_connectionString);
  }
}