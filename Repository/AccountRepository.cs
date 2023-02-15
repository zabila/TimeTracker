using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public sealed class AccountRepository : RepositoryBase<Account>, IAccountsRepository {
    public AccountRepository(RepositoryContext repositoryContext)
        : base(repositoryContext) {
    }

    public IEnumerable<Account> GetAllAccounts(bool trackChanges) => FindAll(trackChanges).ToList();

    public Account? GetAccount(Guid accountId, bool trackChanges) =>
        FindByCondition(ac => ac.Id.Equals(accountId), trackChanges)
            .SingleOrDefault();

    public async Task<IEnumerable<Account>> GetAllAccountsAsync(bool trackChanges) => await FindAll(trackChanges).ToListAsync();

    public async Task<Account?> GetAccountAsync(Guid accountId, bool trackChanges) =>
        await FindByCondition(ac => ac.Id.Equals(accountId), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateAccount(Account account) => Create(account);
    public void DeleteAccount(Account account) => Delete(account);
}