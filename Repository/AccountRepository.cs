using Contracts;
using Entities.Models;

namespace Repository;

public sealed class AccountRepository : RepositoryBase<Account>, IAccountsRepository
{
    public AccountRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }
}