using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bank.Models;
using Bank.Persistence.Account;
using Bank.Persistence.Currency;
using Bank.Persistence.Transaction;

namespace Bank.Controllers
{
  public class AccountController : ApiController
  {
    /// <summary>
    /// The accounts
    /// </summary>
    private readonly IAccountRepository _accountRepository;

    /// <summary>
    /// The exchange rate service
    /// </summary>
    private readonly IExchangeRateRepository _exchangeRateRepository;

    private ITransactionRepository _transactionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    public AccountController(IAccountRepository accountRepository, IExchangeRateRepository exchangeRateRepository, ITransactionRepository transactionRepository)
    {
      _accountRepository = accountRepository;
      _exchangeRateRepository = exchangeRateRepository;
      _transactionRepository = transactionRepository;
    }

    /// <summary>
    /// Gets the account corresponding to the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    [ResponseType(typeof(Account))]
    public async Task<IHttpActionResult> Get(int id)
    {
      var account = (await _accountRepository.GetAccountsAsync(new List<int> { id })).First();
      account.Transactions = (await _transactionRepository.GetTransactionByAccountIdsAsync(new List<int> {id})).ToList();
      return Ok(account);
    }

    //public IHttpActionResult Post([FromBody] IList<Account> accounts)
    //{
    //  foreach (var account in accounts)
    //  {
    //    _accountRepository.Save(account);
    //  }

    //  return Ok();
    //}

    /// <summary>
    /// Deletes the account corresponding to the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public async Task<IHttpActionResult> Delete(int id)
    {
      var account = (await _accountRepository.GetAccountsAsync(new List<int> { id })).First();
      if (account == null) return NotFound();
      {
        var controller = new DepositController(_accountRepository, _exchangeRateRepository)
        {
          Request = new HttpRequestMessage(),
          Configuration = new HttpConfiguration()
        };

        await controller.Post(new Deposit
        {
          AccountId = id,
          Amount = -account.Balance,
          Currency = account.Currency
        });

        account.Status = Status.Closed;

        return Ok(account);
      }
    }
  }
}