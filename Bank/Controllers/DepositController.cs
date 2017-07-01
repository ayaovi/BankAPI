using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Bank.Models;
using Bank.Persistence;

namespace Bank.Controllers
{
  public class DepositController : ApiController
  {
    /// <summary>
    /// The accounts
    /// </summary>
    private readonly IAccountRepository _accounts;

    /// <summary>
    /// The rates
    /// </summary>
    private readonly IExchangeRateRepository _rates;

    /// <summary>
    /// Initializes a new instance of the <see cref="DepositController"/> class.
    /// </summary>
    public DepositController()
    {
      _accounts = new AccountRepository();
      _rates = new ExchangeRateRepository();
    }

    /// <summary>
    /// Posts the specified deposit.
    /// </summary>
    /// <param name="deposit">The deposit.</param>
    /// <returns></returns>
    [ResponseType(typeof(Account))]
    public IHttpActionResult Post([FromBody]Deposit deposit)
    {
      var account = _accounts.GetAccount(deposit.AccountId);
      if (account == null) return Content(HttpStatusCode.NotFound, new { Message = $"Account with ID: {deposit.AccountId} does not exist." });
      {
        if (account.Status == Status.Closed) return Content(HttpStatusCode.BadRequest, new { Message = $"Account with ID: {deposit.AccountId} is CLOSED." });
        {
          var amount = deposit.Amount * _rates.GetExchangeRate(deposit.Currency, account.Currency);
          if (amount < 0 && account.Balance < -amount)
            return Content(HttpStatusCode.BadRequest, new
            {
              Message = $"Insufficient Fund in Account with ID {account.Id}.",
              AccountBalance = account.Balance,
              Withdraw = -amount
            });
          account.Balance += amount;
          account.Transactions.Add(new Transaction {Amount = amount, Date = DateTime.Now});
          return Ok(account);
        }
      }
    }
  }
}