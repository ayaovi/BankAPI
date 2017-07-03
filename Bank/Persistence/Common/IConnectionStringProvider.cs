using System.Threading.Tasks;

namespace Bank.Persistence.Common
{
  public interface IConnectionStringProvider
  {
    /// <summary>
    /// Gets the connection string asynchronously.
    /// </summary>
    /// <returns></returns>
    Task<string> GetConnectionStringAsync();
  }
}