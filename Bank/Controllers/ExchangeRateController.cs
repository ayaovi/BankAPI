using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Bank.Models;
using Bank.Persistence;

namespace Bank.Controllers
{
  public class ExchangeRateController : ApiController
  {
    /// <summary>
    /// The rates
    /// </summary>
    private readonly IExchangeRateRepository _rates;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExchangeRateController"/> class.
    /// </summary>
    public ExchangeRateController()
    {
      _rates = new ExchangeRateRepository();
    }

    /// <summary>
    /// Gets the exchange rate from the two specified currencies.
    /// </summary>
    /// <param name="from">The origin currency.</param>
    /// <param name="to">The destination currency.</param>
    /// <returns></returns>
    [ResponseType(typeof(Currency))]
    public IHttpActionResult Get(Currency from, Currency to)
    {
      return Ok(_rates.GetExchangeRate(from, to));
    }

    /// <summary>
    /// Posts the specified exchange rates.
    /// </summary>
    /// <param name="rates">The rates.</param>
    /// <returns></returns>
    public IHttpActionResult Post(IDictionary<Currency, decimal> rates)
    {
      foreach (var currency in rates.Keys)
      {
        _rates.Save(currency, rates[currency]);
      }
      return Ok();
    }
  }
}