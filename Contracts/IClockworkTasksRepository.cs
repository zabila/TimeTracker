using Entities.Models;

namespace Contracts;

public interface IClockworkTasksRepository
{
    IEnumerable<ClockworkTask> GetAllClockworkTasks(Guid accountId, bool trackChanges);
    ClockworkTask? GetClockworkTask(Guid accountId, Guid id, bool trackChanges);
}