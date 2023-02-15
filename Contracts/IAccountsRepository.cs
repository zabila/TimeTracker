using Entities.Models;

namespace Contracts;

public interface IAccountsRepository {
    IEnumerable<Account> GetAllAccounts(bool trackChanges);
    Account? GetAccount(Guid accountId, bool trackChanges);
    Task<IEnumerable<Account>> GetAllAccountsAsync(bool trackChanges);
    Task<Account?> GetAccountAsync(Guid accountId, bool trackChanges);

    void CreateAccount(Account account);
    void DeleteAccount(Account account);
}