using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IClockworkTasksService
{
    IEnumerable<ClockworkTaskDto> GetAllClockworkTasks(Guid accountId, bool trackChanges);
    ClockworkTaskDto? GetClockworkTask(Guid accountId, Guid id, bool trackChanges);
}