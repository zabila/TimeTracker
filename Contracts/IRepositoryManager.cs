namespace Contracts;

public interface IRepositoryManager
{
    IAccountsRepository Accounts { get; }
    void Save();
}