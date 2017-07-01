using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Bank.Models;

namespace Bank.Controllers
{
  public class TransferController : ApiController
  {
    /// <summary>
    /// Posts the specified transfer.
    /// </summary>
    /// <param name="transfer">The transfer.</param>
    /// <returns></returns>
    [ResponseType(typeof(Account))]
    public IHttpActionResult Post([FromBody]Transfer transfer)
    {
      var controller = new DepositController
      {
        Request = new HttpRequestMessage(),
        Configuration = new HttpConfiguration()
      };

      var actionResult = controller.Post(new Deposit
      {
        AccountId = transfer.OriginAccountId,
        Amount = -transfer.Amount,
        Currency = transfer.Currency
      });

      if (actionResult.GetType() != typeof(OkNegotiatedContentResult<Account>)) return actionResult;
      return controller.Post(new Deposit
      {
        AccountId = transfer.DestinationAccountId,
        Amount = transfer.Amount,
        Currency = transfer.Currency
      });
    }
  }
}