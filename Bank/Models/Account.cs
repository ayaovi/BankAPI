using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bank.Models
{
  public class Account
  {
    /// <summary>
    /// The account Id.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// The account balance.
    /// </summary>
    [Required]
    public decimal Balance { get; set; }

    /// <summary>
    /// The account currency.
    /// </summary>
    [Required]
    public Currency Currency { get; set; } = Currency.Rand;

    /// <summary>
    /// The account status.
    /// </summary>
    [Required]
    public Status Status { get; set; } = Status.Active;

    /// <summary>
    /// The account's transaction history.
    /// </summary>
    public IList<Transaction> Transactions { get; set; } = new List<Transaction>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    protected bool Equals(Account other)
    {
      return Id == other.Id && 
             Balance == other.Balance && 
             Currency == other.Currency && 
             Equals(Transactions, other.Transactions);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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