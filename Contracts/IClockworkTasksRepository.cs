using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IClockworkTasksRepository
{
    IEnumerable<ClockworkTask> GetAllClockworkTasks(Guid accountId, bool trackChanges);
    ClockworkTask? GetClockworkTask(Guid accountId, Guid id, bool trackChanges);
    IEnumerable<ClockworkTask> GetClockworkTasksByIds(Guid accountId, IEnumerable<Guid> ids, bool trackChanges);
    Task<IEnumerable<ClockworkTask>> GetAllClockworkTasksAsync(Guid accountId, ClockworkTasksParameters clockworkTasksParameters, bool trackChanges);
    Task<ClockworkTask?> GetClockworkTaskAsync(Guid accountId, Guid id, bool trackChanges);
    Task<IEnumerable<ClockworkTask>> GetClockworkTasksByIdsAsync(Guid accountId, IEnumerable<Guid> ids, bool trackChanges);
    void CreateClockworkTask(Guid accountId, ClockworkTask clockworkTask);
    void DeleteClockworkTask(ClockworkTask clockworkTask);
}