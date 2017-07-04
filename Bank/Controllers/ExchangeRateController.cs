using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bank.Models;
using Bank.Persistence.Currency;

namespace Bank.Controllers
{
  public class ExchangeRateController : ApiController
  {
    /// <summary>
    /// The exchange rate service
    /// </summary>
    private readonly IExchangeRateRepository _exchangeRateService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExchangeRateController"/> class.
    /// </summary>
    /// <param name="exchangeRateService">The exchange rate service.</param>
    public ExchangeRateController(IExchangeRateRepository exchangeRateService)
    {
      _exchangeRateService = exchangeRateService;
    }

    /// <summary>
    /// Gets the exchange rate from the two specified currencies.
    /// </summary>
    /// <param name="from">The origin currency.</param>
    /// <param name="to">The destination currency.</param>
    /// <returns></returns>
    [ResponseType(typeof(Currency))]
    public async Task<IHttpActionResult> Get(Currency from, Currency to)
    {
      return Ok((await _exchangeRateService.GetExchangeRateAsync(from, to)).First());
    }
  }
}