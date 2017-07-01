using System.Collections.Generic;
using System.Linq;

namespace Bank.Models
{
  public class Account
  {
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public Currency Currency { get; set; } = Currency.Rand;
    public Status Status { get; set; } = Status.Active;
    public IList<Transaction> Transactions { get; set; } = new List<Transaction>();

    public override bool Equals(object obj)
    {
      // Check for null values and compare run-time types.
      if (obj == null || GetType() != obj.GetType()) return false;

      var account = (Account)obj;
      return Id == account.Id && 
             Balance == account.Balance && 
             Currency == account.Currency && 
             Transactions.All(transaction => account.Transactions.Contains(transaction));
    }

    protected bool Equals(Account other)
    {
      return Id == other.Id && 
             Balance == other.Balance && 
             Currency == other.Currency && 
             Equals(Transactions, other.Transactions);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = Id;
        hashCode = (hashCode * 397) ^ Balance.GetHashCode();
        hashCode = (hashCode * 397) ^ (int) Currency;
        hashCode = (hashCode * 397) ^ (Transactions?.GetHashCode() ?? 0);
        return hashCode;
      }
    }
  }
}