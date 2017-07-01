﻿using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Bank.Models;
using Bank.Persistence;

namespace Bank.Controllers
{
  public class AccountController : ApiController
  {
    /// <summary>
    /// The accounts
    /// </summary>
    private readonly IAccountRepository _accounts;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    public AccountController()
    {
      _accounts = new AccountRepository();
    }

    /// <summary>
    /// Gets the account corresponding to the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    [ResponseType(typeof(Account))]
    public IHttpActionResult Get(int id)
    {
      var account = _accounts.GetAccount(id);
      return account != null ? (IHttpActionResult) Ok(account) : NotFound();
    }

    public IHttpActionResult Post([FromBody] IList<Account> accounts)
    {
      foreach (var account in accounts)
      {
        _accounts.Save(account);
      }

      return Ok();
    }

    /// <summary>
    /// Deletes the account corresponding to the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public IHttpActionResult Delete(int id)
    {
      var account = _accounts.GetAccount(id);
      if (account == null) return NotFound();
      {
        var controller = new DepositController
        {
          Request = new HttpRequestMessage(),
          Configuration = new HttpConfiguration()
        };
        
        controller.Post(new Deposit
        {
          AccountId = id, Amount = -account.Balance, Currency = account.Currency
        });

        account.Status = Status.Closed;

        return Ok(account);
      }
    }
  }
}