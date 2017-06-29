using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Bank.Models;

namespace Bank.Controllers
{
  public class BankController : ApiController
  {
    private static readonly Lazy<ConcurrentDictionary<int, Account>> LazyAccounts = new Lazy<ConcurrentDictionary<int, Account>>(InitialiseDictionary, true);

    private static ConcurrentDictionary<int, Account> InitialiseDictionary()
    {
      return new ConcurrentDictionary<int, Account>(new[]
      {
        new KeyValuePair<int, Account>(1, new Account {Id = 1, Balance = 1000}),
        new KeyValuePair<int, Account>(2, new Account {Id = 2, Balance = 1000}),
        new KeyValuePair<int, Account>(3, new Account {Id = 3, Balance = 1000})
      });
    }
    

    [ResponseType(typeof(Account))]
    public IHttpActionResult Get(int id)
    {
      if (!LazyAccounts.Value.ContainsKey(id)) return NotFound();

      return Ok(LazyAccounts.Value[id]);
    }

    public IHttpActionResult Post(int id, Account account)
    {
      if (id != account.Id)
      {
        return Content(HttpStatusCode.BadRequest, new
        {
          Message = "Mismatching Ids",
          UriID = id,
          AccountID = account.Id
        });
      }
      LazyAccounts.Value[id] = account;
      return Ok();
    }
  }
}
