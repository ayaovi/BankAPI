using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Bank.Models;
using Bank.Persistence;

namespace Bank.Controllers
{
  public class ExchangeRateController : ApiController
  {
    private readonly IExchangeRateRepository _rates;

    public ExchangeRateController()
    {
      _rates = new ExchangeRateRepository();
    }

    [ResponseType(typeof(Currency))]
    public IHttpActionResult Get(Currency from, Currency to)
    {
      return Ok(_rates.GetExchangeRate(from, to));
    }

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