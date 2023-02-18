using System.Dynamic;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class ClockworkTasksService : IClockworkTasksService {
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDataShaper<ClockworkTaskDto> _dataShaper;

    public ClockworkTasksService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IDataShaper<ClockworkTaskDto> dataShaper) {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _dataShaper = dataShaper;
    }

    private async Task CheckIfAccountExitsAsync(Guid accountId, bool trackChanges) {
        var account = await _repository.Accounts.GetAccountAsync(accountId, trackChanges);
        if (account is null)
            throw new AccountNotFoundException(accountId);
    }

    private void CheckIfAccountExits(Guid accountId, bool trackChanges) {
        var account = _repository.Accounts.GetAccount(accountId, trackChanges);
        if (account is null)
            throw new AccountNotFoundException(accountId);
    }

    private async Task<Account> GetAccountAndCheckIfItExistsAsync(Guid accountId, bool trackChanges) {
        var account = await _repository.Accounts.GetAccountAsync(accountId, trackChanges);
        if (account is null)
            throw new AccountNotFoundException(accountId);
        return account;
    }

    private Account GetAccountAndCheckIfItExists(Guid accountId, bool trackChanges) {
        var account = _repository.Accounts.GetAccount(accountId, trackChanges);
        if (account is null)
            throw new AccountNotFoundException(accountId);
        return account;
    }

    private ClockworkTask GetClockworkTaskAndCheckIfItExists(Guid accountId, Guid id, bool trackChanges) {
        CheckIfAccountExits(accountId, trackChanges);
        var clockworkTask = _repository.ClockworkTasks.GetClockworkTask(accountId, id, trackChanges);
        if (clockworkTask is null)
            throw new ClockworkTaskNotFoundException(id);
        return clockworkTask;
    }

    private async Task<ClockworkTask> GetClockworkTaskAndCheckIfItExistsAsync(Guid accountId, Guid id, bool trackChanges) {
        await CheckIfAccountExitsAsync(accountId, trackChanges);
        var clockworkTask = await _repository.ClockworkTasks.GetClockworkTaskAsync(accountId, id, trackChanges);
        if (clockworkTask is null)
            throw new ClockworkTaskNotFoundException(id);
        return clockworkTask;
    }

    public IEnumerable<ClockworkTaskDto> GetAllClockworkTasks(Guid accountId, bool trackChanges) {
        var clockworkTasks = _repository.ClockworkTasks.GetAllClockworkTasks(accountId, trackChanges);
        var clockworkTasksDto = _mapper.Map<IEnumerable<ClockworkTaskDto>>(clockworkTasks);
        return clockworkTasksDto;
    }

    public ClockworkTaskDto? GetClockworkTask(Guid accountId, Guid id, bool trackChanges) {
        var clockworkTask = GetClockworkTaskAndCheckIfItExists(accountId, id, trackChanges);
        var clockworkTaskDto = _mapper.Map<ClockworkTaskDto>(clockworkTask);
        return clockworkTaskDto;
    }

    public ClockworkTaskDto CreateClockworkTask(Guid accountId, ClockworkTaskForCreationDto clockworkTask, bool trackChanges) {
        CheckIfAccountExits(accountId, trackChanges);

        var clockworkTaskEntity = _mapper.Map<ClockworkTask>(clockworkTask);
        _repository.ClockworkTasks.CreateClockworkTask(accountId, clockworkTaskEntity);
        _repository.Save();

        var clockworkTaskDto = _mapper.Map<ClockworkTaskDto>(clockworkTaskEntity);
        return clockworkTaskDto;
    }

    public void DeleteClockworkTask(Guid accountId, Guid id, bool trackChanges) {
        var accountForTask = GetClockworkTaskAndCheckIfItExists(accountId, id, trackChanges);
        _repository.ClockworkTasks.DeleteClockworkTask(accountForTask);
        _repository.Save();
    }

    public void UpdateClockworkTask(Guid accountId, Guid id, ClockworkTaskForUpdateDto clockworkTaskForUpdateDto, bool accTrackChanges, bool tskTrackChanges) {
        var clockworkTask = GetClockworkTaskAndCheckIfItExists(accountId, id, tskTrackChanges);
        _mapper.Map(clockworkTaskForUpdateDto, clockworkTask);
        _repository.Save();
    }

    public async Task<(IEnumerable<ExpandoObject> clockworkTaskDtos, MetaData metaData )>
        GetAllClockworkTasksAsync(Guid accountId, ClockworkTasksParameters clockworkTasksParameters, bool trackChanges) {
        await CheckIfAccountExitsAsync(accountId, trackChanges);

        if (!clockworkTasksParameters.ValidStartedDateTimeRange)
            throw new StartedDataTimeRangeBadRequestException(clockworkTasksParameters.FromStartedDateTime, clockworkTasksParameters.ToStartedDateTime);

        var clockworkTasksWithMetaData = await _repository.ClockworkTasks.GetAllClockworkTasksAsync(accountId, clockworkTasksParameters, trackChanges);
        var clockworkTasksDto = _mapper.Map<IEnumerable<ClockworkTaskDto>>(clockworkTasksWithMetaData);
        var shapedClockworkTasksDto = _dataShaper.ShapeData(clockworkTasksDto, clockworkTasksParameters.Fields!);
        return (clockworkTaskDtos: shapedClockworkTasksDto, metaData: clockworkTasksWithMetaData.MetaData);
    }

    public async Task<ClockworkTaskDto?> GetClockworkTaskAsync(Guid accountId, Guid id, bool trackChanges) {
        var clockworkTask = await GetClockworkTaskAndCheckIfItExistsAsync(accountId, id, trackChanges);
        var clockworkTaskDto = _mapper.Map<ClockworkTaskDto>(clockworkTask);
        return clockworkTaskDto;
    }

    public async Task<IEnumerable<ClockworkTaskDto>> GetClockworkTasksCollectionAsync(Guid accountId, IEnumerable<Guid> ids, bool trackChanges) {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var clockworkTasks = await _repository.ClockworkTasks.GetClockworkTasksByIdsAsync(accountId, ids, false);
        if (clockworkTasks.Count() != ids.Count())
            throw new CollectionByIdsBadRequestException();

        var clockworkTasksDto = _mapper.Map<IEnumerable<ClockworkTaskDto>>(clockworkTasks);
        return clockworkTasksDto;
    }

    public async Task<ClockworkTaskDto> CreateClockworkTaskAsync(Guid accountId, ClockworkTaskForCreationDto clockworkTask, bool trackChanges) {
        await CheckIfAccountExitsAsync(accountId, trackChanges);

        var clockworkTaskEntity = _mapper.Map<ClockworkTask>(clockworkTask);
        _repository.ClockworkTasks.CreateClockworkTask(accountId, clockworkTaskEntity);
        await _repository.SaveAsync();

        var clockworkTaskDto = _mapper.Map<ClockworkTaskDto>(clockworkTaskEntity);
        return clockworkTaskDto;
    }

    public async Task DeleteClockworkTaskAsync(Guid accountId, Guid id, bool trackChanges) {
        var accountForTask = await GetClockworkTaskAndCheckIfItExistsAsync(accountId, id, trackChanges);
        _repository.ClockworkTasks.DeleteClockworkTask(accountForTask);
        await _repository.SaveAsync();
    }

    public async Task UpdateClockworkTaskAsync(Guid accountId, Guid id, ClockworkTaskForUpdateDto clockworkTaskForUpdateDto, bool accTrackChanges, bool
        tskTrackChanges) {
        await CheckIfAccountExitsAsync(accountId, accTrackChanges);

        var clockworkTask = await _repository.ClockworkTasks.GetClockworkTaskAsync(accountId, id, tskTrackChanges);
        if (clockworkTask is null)
            throw new ClockworkTaskNotFoundException(id);

        _mapper.Map(clockworkTaskForUpdateDto, clockworkTask);
        await _repository.SaveAsync();
    }

    public IEnumerable<ClockworkTaskDto> GetClockworkTasksCollection(Guid accountId, IEnumerable<Guid> ids, bool trackChanges) {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var clockworkTasks = _repository.ClockworkTasks.GetClockworkTasksByIds(accountId, ids, false);
        if (clockworkTasks.Count() != ids.Count())
            throw new CollectionByIdsBadRequestException();

        var clockworkTasksDto = _mapper.Map<IEnumerable<ClockworkTaskDto>>(clockworkTasks);
        return clockworkTasksDto;
    }
}