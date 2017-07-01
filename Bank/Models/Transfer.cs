namespace Bank.Models
{
  public class Transfer
  {
    public int OriginAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
  }
}