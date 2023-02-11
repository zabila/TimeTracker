using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IClockworkTasksService
{
    IEnumerable<ClockworkTaskDto> GetAllClockworkTasks(Guid accountId, bool trackChanges);
    ClockworkTaskDto? GetClockworkTask(Guid accountId, Guid id, bool trackChanges);
    ClockworkTaskDto CreateClockworkTask(Guid accountId, ClockworkTaskForCreationDto clockworkTask, bool trackChanges);
    void DeleteClockworkTask(Guid accountId, Guid id, bool trackChanges);
    void UpdateClockworkTask(Guid accountId, Guid id, ClockworkTaskForUpdateDto clockworkTaskForUpdateDto, bool accTrackChanges, bool tskTrackChanges);
    IEnumerable<ClockworkTaskDto> GetClockworkTasksCollection(Guid accountId, IEnumerable<Guid> ids, bool trackChanges);
}