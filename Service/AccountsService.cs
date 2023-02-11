using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Shared.DataTransferObjects;
using Service.Contracts;

namespace Service;

internal sealed class AccountsService : IAccountsService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public AccountsService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<AccountDto> GetAllAccounts(bool trackChanges)
    {
        var accounts = _repository.Accounts.GetAllAccounts(trackChanges);
        var accountsDto = _mapper.Map<IEnumerable<AccountDto>>(accounts);
        return accountsDto;
    }

    public AccountDto? GetAccount(Guid accountId, bool trackChanges)
    {
        var account = _repository.Accounts.GetAccount(accountId, trackChanges);
        if (account is null)
        {
            _logger.LogInfo($"Account with id: {accountId} doesn't exist in the database.");
            return null;
        }

        var accountDto = _mapper.Map<AccountDto>(account);
        return accountDto;
    }

    public AccountDto CreateAccount(AccountForCreationDto account)
    {
        var accountEntity = _mapper.Map<Account>(account);

        _repository.Accounts.CreateAccount(accountEntity);
        _repository.Save();

        var accountToReturn = _mapper.Map<AccountDto>(accountEntity);
        return accountToReturn;
    }

    public void DeleteAccount(Guid accountId)
    {
        var account = _repository.Accounts.GetAccount(accountId, false);
        if (account is null)
            throw new AccountNotFoundException(accountId);

        _repository.Accounts.DeleteAccount(account);
        _repository.Save();
    }

    public void UpdateAccount(Guid accountId, AccountForUpdateDto accountForUpdateDto, bool trackChanges)
    {
        var account = _repository.Accounts.GetAccount(accountId, trackChanges);
        if (account is null)
            throw new AccountNotFoundException(accountId);

        _mapper.Map(accountForUpdateDto, account);
        _repository.Save();
    }
}