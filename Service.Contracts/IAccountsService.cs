using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IAccountsService
{
    IEnumerable<AccountDto> GetAllAccounts(bool trackChanges);

    AccountDto? GetAccount(Guid accountId, bool trackChanges);

    AccountDto CreateAccount(AccountForCreationDto account);
    void DeleteAccount(Guid accountId);
}