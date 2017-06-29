using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Bank.Models;
using Bank.Persistence;

namespace Bank.Controllers
{
  public class AccountController : ApiController
  {
    private readonly IAccountRepository _accounts;

    public AccountController()
    {
      _accounts = new AccountRepository();
    }

    [ResponseType(typeof(Account))]
    public IHttpActionResult Get(int id)
    {
      var account = _accounts.GetAccount(id);
      return account != null ? (IHttpActionResult) Ok(account) : NotFound();
    }

    public IHttpActionResult Post(IList<Account> accounts)
    {
      foreach (var account in accounts)
      {
        _accounts.Save(account);
      }
      //_accounts.Save(account);
      //if (id != account.Id)
      //{
      //  return Content(HttpStatusCode.BadRequest, new
      //  {
      //    Message = "Mismatching Ids",
      //    UriID = id,
      //    AccountID = account.Id
      //  });
      //}
      //LazyAccounts.Value[id] = account;
      return Ok();
    }
  }
}
