using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace TimeTracker.Presentation.Controllers;

[Route("api/accounts")]
[ApiController]
public class TimeTrackerController : ControllerBase
{
    private readonly IServiceManager _service;

    public TimeTrackerController(IServiceManager serviceManager) => _service = serviceManager;

    [HttpGet]
    public IActionResult GetAccounts()
    {
        var accounts = _service.Accounts.GetAllAccounts(false);
        return Ok(accounts);
    }

    [HttpGet("{id:Guid}")]
    public IActionResult GetAccount(Guid id)
    {
        var account = _service.Accounts.GetAccount(id, false);
        if (account is null)
            throw new AccountNotFoundException(id);

        return Ok(account);
    }
}