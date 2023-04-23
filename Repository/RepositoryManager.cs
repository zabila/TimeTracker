using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager {
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IAccountsRepository> _accountRepository;
    private readonly Lazy<IClockworkTasksRepository> _clockworkTasksRepository;

    public RepositoryManager(RepositoryContext repositoryContext) {
        _repositoryContext = repositoryContext;
        _accountRepository = new Lazy<IAccountsRepository>(() => new AccountRepository(repositoryContext));
        _clockworkTasksRepository = new Lazy<IClockworkTasksRepository>(() => new ClockworkTasksRepository(repositoryContext));
    }

    public IAccountsRepository Accounts {
        get {
            return _accountRepository.Value;
        }
    }
    public IClockworkTasksRepository ClockworkTasks {
        get {
            return _clockworkTasksRepository.Value;
        }
    }

    public void Save() {
        _repositoryContext.SaveChanges();
    }
    public async Task SaveAsync() {
        await _repositoryContext.SaveChangesAsync();
    }
}