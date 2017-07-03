using System.Collections.Generic;
using System.Data;

namespace Bank.Persistence.Utilities
{
  public static class Util
  {
    /// <summary>
    /// IEnumerable to the int table type.
    /// </summary>
    /// <param name="accountIds">The account ids.</param>
    /// <returns></returns>
    public static DataTable ToIntTable(IEnumerable<int> accountIds)
    {
      var dataTable = new DataTable("dbo.tp_IntTable");
      dataTable.Columns.Add("Value", typeof(int));

      foreach (var accountId in accountIds)
      {
        dataTable.Rows.Add(accountId);
      }
      return dataTable;
    }
  }
}