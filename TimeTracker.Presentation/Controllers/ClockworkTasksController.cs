using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

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

    [HttpGet("{id:Guid}", Name = "GetClockworkForAccount")]
    public IActionResult GetClockworkTaskForAccount(Guid accountId, Guid id)
    {
        var clockworkTask = _service.ClockworkTasks.GetClockworkTask(accountId, id, false);
        return Ok(clockworkTask);
    }

    [HttpPost]
    public IActionResult CreateClockworkTask(Guid accountId, [FromBody] ClockworkTaskForCreationDto? clockworkTask)
    {
        if (clockworkTask is null)
            return BadRequest("ClockworkTaskForCreationDto object is null");

        var clockworkToReturn = _service.ClockworkTasks.CreateClockworkTask(accountId, clockworkTask, false);
        return CreatedAtRoute("GetClockworkForAccount", new { accountId, id = clockworkToReturn.Id }, clockworkToReturn);
    }
}