using Microsoft.Practices.Unity;
using System.Web.Http;
using Bank.Persistence.Account;
using Bank.Persistence.Common;
using Bank.Persistence.Currency;
using Bank.Persistence.Transaction;
using Unity.WebApi;

namespace Bank
{
  public static class UnityConfig
  {
    /// <summary>
    /// The container
    /// </summary>
    private static UnityContainer _container;

    /// <summary>
    /// Registers the components.
    /// </summary>
    public static void RegisterComponents()
    {
      _container = new UnityContainer();

      // register all your components with the _container here
      // it is NOT necessary to register your controllers

      // e.g. _container.RegisterType<ITestService, TestService>();

      GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(_container);
      RegisterDataAccessInstance();
    }

    /// <summary>
    /// Registers the data access instance.
    /// </summary>
    public static void RegisterDataAccessInstance()
    {
      _container.RegisterType<IConnectionStringProvider, ConnectionStringProvider>();

      _container.RegisterType<IAccountRepository, AccountRepository>();
      _container.RegisterType<ITransactionRepository, TransactionRepository>();
      _container.RegisterType<IExchangeRateRepository, ExchangeRateRepository>();
    }

    /// <summary>
    /// Resoloves this instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Resolove<T>() => _container.Resolve<T>();
  }
}