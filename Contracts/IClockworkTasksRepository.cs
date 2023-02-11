using Entities.Models;

namespace Contracts;

public interface IClockworkTasksRepository
{
    IEnumerable<ClockworkTask> GetAllClockworkTasks(Guid accountId, bool trackChanges);
    ClockworkTask? GetClockworkTask(Guid accountId, Guid id, bool trackChanges);
    void CreateClockworkTask(Guid accountId, ClockworkTask clockworkTask);
    IEnumerable<ClockworkTask> GetClockworkTasksByIds(Guid accountId, IEnumerable<Guid> ids, bool trackChanges);
}