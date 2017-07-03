using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bank.Models;
using Bank.Persistence.Account;
using Bank.Persistence.Currency;

namespace Bank.Controllers
{
  public class DepositController : ApiController
  {
    /// <summary>
    /// The account service
    /// </summary>
    private readonly IAccountRepository _accountRepository;

    /// <summary>
    /// The exchange rate service
    /// </summary>
    private readonly IExchangeRateRepository _exchangeRateRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DepositController"/> class.
    /// </summary>
    /// <param name="accountRepository">The account service.</param>
    /// <param name="exchangeRateRepository">The exchange rate service.</param>
    public DepositController(IAccountRepository accountRepository, IExchangeRateRepository exchangeRateRepository)
    {
      _accountRepository = accountRepository;
      _exchangeRateRepository = exchangeRateRepository;
    }

    /// <summary>
    /// Posts the specified deposit.
    /// </summary>
    /// <param name="deposit">The deposit.</param>
    /// <returns></returns>
    [ResponseType(typeof(Account))]
    public async Task<IHttpActionResult> Post([FromBody]Deposit deposit)
    {
      var account = (await _accountRepository.GetAccountsAsync(new List<int>(deposit.AccountId))).First();
      if (account == null) return Content(HttpStatusCode.NotFound, new { Message = $"Account with ID: {deposit.AccountId} does not exist." });
      {
        if (account.Status == Status.Closed) return Content(HttpStatusCode.BadRequest, new { Message = $"Account with ID: {deposit.AccountId} is CLOSED." });
        {
          var amount = deposit.Amount * (await _exchangeRateRepository.GetExchangeRateAsync(deposit.Currency, account.Currency)).First();
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