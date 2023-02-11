using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class ClockworkTasksService : IClockworkTasksService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ClockworkTasksService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<ClockworkTaskDto> GetAllClockworkTasks(Guid accountId, bool trackChanges)
    {
        var clockworkTasks = _repository.ClockworkTasks.GetAllClockworkTasks(accountId, trackChanges);
        var clockworkTasksDto = _mapper.Map<IEnumerable<ClockworkTaskDto>>(clockworkTasks);
        return clockworkTasksDto;
    }

    public ClockworkTaskDto? GetClockworkTask(Guid accountId, Guid id, bool trackChanges)
    {
        var account = _repository.Accounts.GetAccount(accountId, trackChanges);
        if (account is null)
            throw new AccountNotFoundException(accountId);

        var clockworkTask = _repository.ClockworkTasks.GetClockworkTask(accountId, id, trackChanges);
        if (clockworkTask is null)
            throw new ClockworkTaskNotFoundException(id);

        var clockworkTaskDto = _mapper.Map<ClockworkTaskDto>(clockworkTask);
        return clockworkTaskDto;
    }

    public ClockworkTaskDto CreateClockworkTask(Guid accountId, ClockworkTaskForCreationDto clockworkTask, bool trackChanges)
    {
        var account = _repository.Accounts.GetAccount(accountId, trackChanges);
        if (account is null)
            throw new AccountNotFoundException(accountId);

        var clockworkTaskEntity = _mapper.Map<ClockworkTask>(clockworkTask);
        _repository.ClockworkTasks.CreateClockworkTask(accountId, clockworkTaskEntity);
        _repository.Save();

        var clockworkTaskDto = _mapper.Map<ClockworkTaskDto>(clockworkTaskEntity);
        return clockworkTaskDto;
    }

    public void DeleteClockworkTask(Guid accountId, Guid id, bool trackChanges)
    {
        var account = _repository.Accounts.GetAccount(accountId, trackChanges);
        if (account is null)
            throw new AccountNotFoundException(accountId);

        var accountForTask = _repository.ClockworkTasks.GetClockworkTask(accountId, id, trackChanges);
        if (accountForTask is null)
            throw new ClockworkTaskNotFoundException(id);

        _repository.ClockworkTasks.DeleteClockworkTask(accountForTask);
        _repository.Save();
    }

    public void UpdateClockworkTask(Guid accountId, Guid id, ClockworkTaskForUpdateDto clockworkTaskForUpdateDto, bool accTrackChanges, bool tskTrackChanges)
    {
        var account = _repository.Accounts.GetAccount(accountId, accTrackChanges);
        if (account is null)
            throw new AccountNotFoundException(accountId);

        var clockworkTask = _repository.ClockworkTasks.GetClockworkTask(accountId, id, tskTrackChanges);
        if (clockworkTask is null)
            throw new ClockworkTaskNotFoundException(id);

        _mapper.Map(clockworkTaskForUpdateDto, clockworkTask);
        _repository.Save();
    }

    public IEnumerable<ClockworkTaskDto> GetClockworkTasksCollection(Guid accountId, IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var clockworkTasks = _repository.ClockworkTasks.GetClockworkTasksByIds(accountId, ids, false);
        if (clockworkTasks.Count() != ids.Count())
            throw new CollectionByIdsBadRequestException();

        var clockworkTasksDto = _mapper.Map<IEnumerable<ClockworkTaskDto>>(clockworkTasks);
        return clockworkTasksDto;
    }
}