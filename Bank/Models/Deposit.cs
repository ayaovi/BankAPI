namespace Bank.Models
{
  public class Deposit
  {
    /// <summary>
    /// Gets or sets the account identifier.
    /// </summary>
    /// <value>
    /// The account identifier.
    /// </value>
    public int AccountId { get; set; }

    /// <summary>
    /// Gets or sets the amount.
    /// </summary>
    /// <value>
    /// The amount.
    /// </value>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    /// <value>
    /// The currency.
    /// </value>
    public Currency Currency { get; set; }
  }
}