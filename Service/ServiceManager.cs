using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Service.DataShaping;
using Shared.DataTransferObjects;

namespace Service;

public class ServiceManager : IServiceManager {
    private readonly Lazy<IAccountsService> _accountsService;
    private readonly Lazy<IClockworkTasksService> _clockworkTasksService;
    private readonly Lazy<IAuthenticationService> _authenticationService;

    public ServiceManager(ILoggerManager logger,
        IRepositoryManager repository,
        IMapper mapper,
        IDataShaper<ClockworkTaskDto> dataShaper,
        IClockworkLinks employeeLinks,
        UserManager<User> userManager,
        IConfiguration configuration) {
        _accountsService = new Lazy<IAccountsService>(() => new AccountsService(logger, repository, mapper));
        _clockworkTasksService = new Lazy<IClockworkTasksService>(() => new ClockworkTasksService(logger, repository, mapper, dataShaper, employeeLinks));
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
    }

    public IAccountsService Accounts {
        get {
            return _accountsService.Value;
        }
    }
    public IClockworkTasksService ClockworkTasks {
        get {
            return _clockworkTasksService.Value;
        }
    }
    public IAuthenticationService Authentication {
        get {
            return _authenticationService.Value;
        }
    }
}