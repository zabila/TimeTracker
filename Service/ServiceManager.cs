using AutoMapper;
using Contracts;
using Service.Contracts;
using Service.DataShaping;
using Shared.DataTransferObjects;

namespace Service;

public class ServiceManager : IServiceManager {
    private readonly Lazy<IAccountsService> _accountsService;
    private readonly Lazy<IClockworkTasksService> _clockworkTasksService;

    public ServiceManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IDataShaper<ClockworkTaskDto> dataShaper,  IClockworkLinks employeeLinks) {
        _accountsService = new Lazy<IAccountsService>(() => new AccountsService(logger, repository, mapper));
        _clockworkTasksService = new Lazy<IClockworkTasksService>(() => new ClockworkTasksService(logger, repository, mapper, dataShaper, employeeLinks));
    }

    public IAccountsService Accounts => _accountsService.Value;
    public IClockworkTasksService ClockworkTasks => _clockworkTasksService.Value;
}