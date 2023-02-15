using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository;

public class ClockworkTasksRepository : RepositoryBase<ClockworkTask>, IClockworkTasksRepository {
    public ClockworkTasksRepository(RepositoryContext repositoryContext)
        : base(repositoryContext) {
    }

    public IEnumerable<ClockworkTask> GetAllClockworkTasks(Guid accountId, bool trackChanges)
        => FindByCondition(ac => ac.AccountId.Equals(accountId), trackChanges).ToList();

    public ClockworkTask? GetClockworkTask(Guid accountId, Guid id, bool trackChanges)
        => FindByCondition(ac => ac.Id.Equals(id) && ac.AccountId.Equals(accountId), trackChanges)
            .SingleOrDefault();

    public async Task<IEnumerable<ClockworkTask>> GetClockworkTasksByIdsAsync(Guid accountId, IEnumerable<Guid> ids, bool trackChanges) {
        var tasksByAccountId = await FindByCondition(ac => ac.AccountId.Equals(accountId), trackChanges).ToListAsync();
        var tasksByIds = tasksByAccountId.Where(id => ids.Contains(id.Id)).ToList();
        return tasksByIds;
    }

    public void CreateClockworkTask(Guid accountId, ClockworkTask clockworkTask) {
        clockworkTask.AccountId = accountId;
        Create(clockworkTask);
    }

    public void DeleteClockworkTask(ClockworkTask clockworkTask) => Delete(clockworkTask);

    public IEnumerable<ClockworkTask> GetClockworkTasksByIds(Guid accountId, IEnumerable<Guid> ids, bool trackChanges) {
        var tasksByAccountId = FindByCondition(ac => ac.AccountId.Equals(accountId), trackChanges).ToList();
        var tasksByIds = tasksByAccountId.Where(id => ids.Contains(id.Id)).ToList();
        return tasksByIds;
    }
    public async Task<IEnumerable<ClockworkTask>> GetAllClockworkTasksAsync(Guid accountId, ClockworkTasksParameters clockworkTasksParameters, bool trackChanges) {
        var tasksByAccountId = await FindByCondition(task => task.AccountId.Equals(accountId), trackChanges)
            .OrderBy(task => task.ClockworkTaskKey)
            .Skip(clockworkTasksParameters.PageSize * (clockworkTasksParameters.PageNumber - 1))
            .Take(clockworkTasksParameters.PageSize)
            .ToListAsync();

        return tasksByAccountId;
    }

    public async Task<ClockworkTask?> GetClockworkTaskAsync(Guid accountId, Guid id, bool trackChanges) =>
        await FindByCondition(ac => ac.Id.Equals(id) && ac.AccountId.Equals(accountId), trackChanges)
            .SingleOrDefaultAsync();
}