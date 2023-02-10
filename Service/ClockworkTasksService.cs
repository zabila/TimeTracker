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
}