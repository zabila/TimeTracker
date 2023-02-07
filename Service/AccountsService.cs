using Contracts;
using Service.Contracts;

namespace Service;

internal sealed class AccountsService : IAccountsService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public AccountsService(ILoggerManager logger, IRepositoryManager repository)
    {
        _logger = logger;
        _repository = repository;
    }
}