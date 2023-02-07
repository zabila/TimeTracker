using Contracts;
using Shared.DataTransferObjects;
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

    public IEnumerable<AccountDto> GetAllAccounts(bool trackChanges)
    {
        var accounts = _repository.Accounts.GetAllAccounts(trackChanges);
        var accountsDto = accounts.Select(ac => new AccountDto(ac.Name, ac.Type));
        return accountsDto;
    }

    public AccountDto? GetAccount(int accountId, bool trackChanges)
    {
        var account = _repository.Accounts.GetAccount(accountId, trackChanges);
        if (account is null)
        {
            _logger.LogInfo($"Account with id: {accountId} doesn't exist in the database.");
            return null;
        }

        var accountDto = new AccountDto(account.Name, account.Type);
        return accountDto;
    }
}