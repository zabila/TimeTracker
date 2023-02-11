using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IClockworkTasksService
{
    IEnumerable<ClockworkTaskDto> GetAllClockworkTasks(Guid accountId, bool trackChanges);
    ClockworkTaskDto? GetClockworkTask(Guid accountId, Guid id, bool trackChanges);
    IEnumerable<ClockworkTaskDto> GetClockworkTasksCollection(Guid accountId, IEnumerable<Guid> ids, bool trackChanges);
    ClockworkTaskDto CreateClockworkTask(Guid accountId, ClockworkTaskForCreationDto clockworkTask, bool trackChanges);
    void DeleteClockworkTask(Guid accountId, Guid id, bool trackChanges);
    void UpdateClockworkTask(Guid accountId, Guid id, ClockworkTaskForUpdateDto clockworkTaskForUpdateDto, bool accTrackChanges, bool tskTrackChanges);


    Task<IEnumerable<ClockworkTaskDto>> GetAllClockworkTasksAsync(Guid accountId, bool trackChanges);
    Task<ClockworkTaskDto?> GetClockworkTaskAsync(Guid accountId, Guid id, bool trackChanges);
    Task<IEnumerable<ClockworkTaskDto>> GetClockworkTasksCollectionAsync(Guid accountId, IEnumerable<Guid> ids, bool trackChanges);
    Task<ClockworkTaskDto> CreateClockworkTaskAsync(Guid accountId, ClockworkTaskForCreationDto clockworkTask, bool trackChanges);
    Task DeleteClockworkTaskAsync(Guid accountId, Guid id, bool trackChanges);
    Task UpdateClockworkTaskAsync(Guid accountId, Guid id, ClockworkTaskForUpdateDto clockworkTaskForUpdateDto, bool accTrackChanges, bool tskTrackChanges);
}