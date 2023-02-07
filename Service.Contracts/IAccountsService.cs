using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IAccountsService
{
    IEnumerable<AccountDto> GetAllAccounts(bool trackChanges);

    AccountDto? GetAccount(int accountId, bool trackChanges);
}