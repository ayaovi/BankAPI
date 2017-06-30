namespace Bank.Models
{
  public class Deposit
  {
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
  }
}