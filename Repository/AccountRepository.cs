using Contracts;
using Entities.Models;

namespace Repository;

public sealed class AccountRepository : RepositoryBase<Account>, IAccountsRepository
{
    public AccountRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public IEnumerable<Account> GetAllAccounts(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(ac => ac.Name)
            .ToList();

    public Account? GetAccount(int accountId, bool trackChanges) =>
        FindByCondition(ac => ac.Id.Equals(accountId), trackChanges)
            .SingleOrDefault();
}