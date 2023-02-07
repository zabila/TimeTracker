using Contracts;
using Entities.Models;

namespace Repository;

public sealed class RepositoryManager: IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IAccountsRepository> _accountRepository;
    
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _accountRepository = new Lazy<IAccountsRepository>(() => new AccountRepository(_repositoryContext));
    }
    
    public IAccountsRepository Accounts => _accountRepository.Value;

    public void Save() => _repositoryContext.SaveChanges();
}