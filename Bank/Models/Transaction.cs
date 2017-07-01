using System;

namespace Bank.Models
{
  public class Transaction
  {
    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    /// <value>
    /// The date.
    /// </value>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the amount.
    /// </summary>
    /// <value>
    /// The amount.
    /// </value>
    public decimal Amount { get; set; }
  }
}