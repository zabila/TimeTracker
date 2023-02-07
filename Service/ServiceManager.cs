using Contracts;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAccountsService> _accountsService;

    public ServiceManager(ILoggerManager logger, IRepositoryManager repository)
    {
        _accountsService = new Lazy<IAccountsService>(() => new AccountsService(logger, repository));
    }

    public IAccountsService Accounts => _accountsService.Value;
}