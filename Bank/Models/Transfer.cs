namespace Bank.Models
{
  public class Transfer
  {
    /// <summary>
    /// Gets or sets the origin account identifier.
    /// </summary>
    /// <value>
    /// The origin account identifier.
    /// </value>
    public int OriginAccountId { get; set; }

    /// <summary>
    /// Gets or sets the destination account identifier.
    /// </summary>
    /// <value>
    /// The destination account identifier.
    /// </value>
    public int DestinationAccountId { get; set; }
    
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