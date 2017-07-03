using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Bank.Models;
using Bank.Persistence.Account;
using Bank.Persistence.Currency;

namespace Bank.Controllers
{
  public class TransferController : ApiController
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
    /// Initializes a new instance of the <see cref="TransferController"/> class.
    /// </summary>
    /// <param name="accountRepository">The account service.</param>
    /// <param name="exchangeRateRepository">The exchange rate service.</param>
    public TransferController(IAccountRepository accountRepository, IExchangeRateRepository exchangeRateRepository)
    {
      _accountRepository = accountRepository;
      _exchangeRateRepository = exchangeRateRepository;
    }

    /// <summary>
    /// Posts the specified transfer.
    /// </summary>
    /// <param name="transfer">The transfer.</param>
    /// <returns></returns>
    [ResponseType(typeof(Account))]
    public async Task<IHttpActionResult> Post([FromBody]Transfer transfer)
    {
      var controller = new DepositController(_accountRepository, _exchangeRateRepository)
      {
        Request = new HttpRequestMessage(),
        Configuration = new HttpConfiguration()
      };

      var actionResult = await controller.Post(new Deposit
      {
        AccountId = transfer.OriginAccountId,
        Amount = -transfer.Amount,
        Currency = transfer.Currency
      });

      if (actionResult.GetType() != typeof(OkNegotiatedContentResult<Account>)) return actionResult;
      return await controller.Post(new Deposit
      {
        AccountId = transfer.DestinationAccountId,
        Amount = transfer.Amount,
        Currency = transfer.Currency
      });
    }
  }
}