using Contracts;
using Entities.Models;

namespace Repository;

public class ClockworkTasksRepository : RepositoryBase<ClockworkTask>, IClockworkTasksRepository
{
    public ClockworkTasksRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public IEnumerable<ClockworkTask> GetAllClockworkTasks(Guid accountId, bool trackChanges)
        => FindByCondition(ac => ac.AccountId.Equals(accountId), trackChanges).ToList();

    public ClockworkTask? GetClockworkTask(Guid accountId, Guid id, bool trackChanges)
        => FindByCondition(ac => ac.Id.Equals(id) && ac.AccountId.Equals(accountId), trackChanges)
            .SingleOrDefault();

    public void CreateClockworkTask(Guid accountId, ClockworkTask clockworkTask)
    {
        clockworkTask.AccountId = accountId;
        Create(clockworkTask);
    }
}