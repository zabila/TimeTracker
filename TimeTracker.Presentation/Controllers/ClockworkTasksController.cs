using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace TimeTracker.Presentation.Controllers;

[Route("api/accounts/{accountId}/clockworktasks")]
[ApiController]
public class ClockworkTasksController : ControllerBase
{
    private readonly IServiceManager _service;

    public ClockworkTasksController(IServiceManager serviceManager) => _service = serviceManager;

    [HttpGet]
    public IActionResult GetClockworkTasksForAccount(Guid accountId)
    {
        var clockworkTasks = _service.ClockworkTasks.GetAllClockworkTasks(accountId, false);
        return Ok(clockworkTasks);
    }

    [HttpGet("{id:Guid}")]
    public IActionResult GetClockworkTaskForAccount(Guid accountId, Guid id)
    {
        var clockworkTask = _service.ClockworkTasks.GetClockworkTask(accountId, id, false);
        return Ok(clockworkTask);
    }
}