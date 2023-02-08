namespace Contracts;

public interface IRepositoryManager
{
    IAccountsRepository Accounts { get; }
    IClockworkTasksRepository ClockworkTasks { get; }
    void Save();
}