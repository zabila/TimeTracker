using Contracts;
using Entities.Models;

namespace Repository;

public class AccountRepository : RepositoryBase<Account>, IAccountsRepository
{
    public AccountRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }
}