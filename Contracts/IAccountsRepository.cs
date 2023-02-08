using Entities.Models;

namespace Contracts;

public interface IAccountsRepository
{
    IEnumerable<Account> GetAllAccounts(bool trackChanges);
    Account? GetAccount(Guid accountId, bool trackChanges);
}