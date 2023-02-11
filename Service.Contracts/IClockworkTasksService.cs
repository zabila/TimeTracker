using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IClockworkTasksService
{
    IEnumerable<ClockworkTaskDto> GetAllClockworkTasks(Guid accountId, bool trackChanges);
    ClockworkTaskDto? GetClockworkTask(Guid accountId, Guid id, bool trackChanges);
    ClockworkTaskDto CreateClockworkTask(Guid accountId, ClockworkTaskForCreationDto clockworkTask, bool trackChanges);
    void DeleteClockworkTask(Guid companyId, Guid id, bool trackChanges);
    IEnumerable<ClockworkTaskDto> GetClockworkTasksCollection(Guid accountId, IEnumerable<Guid> ids, bool trackChanges);
}