using System.Text.Json;
using System.Text.Json.Nodes;
using Shared.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using TimeTracker.Presentation.ModelBinders;

namespace TimeTracker.Presentation.Controllers;

[Route("api/accounts/{accountId:Guid}/clockworktasks")]
[ApiController]
public class ClockworkTasksController : ControllerBase {
    private readonly IServiceManager _service;

    public ClockworkTasksController(IServiceManager serviceManager) => _service = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetClockworkTasksForAccount(Guid accountId, [FromQuery] ClockworkTasksParameters clockworkTasksParameters) {
        var pagesResult = await _service.ClockworkTasks.GetAllClockworkTasksAsync(accountId, clockworkTasksParameters, false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagesResult.metaData));
        return Ok(pagesResult.clockworkTaskDtos);
    }

    [HttpGet("{id:Guid}", Name = "GetClockworkForAccount")]
    public async Task<IActionResult> GetClockworkTaskForAccount(Guid accountId, Guid id) {
        var clockworkTask = await _service.ClockworkTasks.GetClockworkTaskAsync(accountId, id, false);
        return Ok(clockworkTask);
    }

    [HttpGet("collection/({ids})", Name = "ClockworkTasksCollection")]
    public async Task<IActionResult> GetCollectionByIds(Guid accountId, [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids) {
        var tasks = await _service.ClockworkTasks.GetClockworkTasksCollectionAsync(accountId, ids, false);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClockworkTask(Guid accountId, [FromBody] ClockworkTaskForCreationDto? clockworkTask) {
        if (clockworkTask is null)
            return BadRequest("ClockworkTaskForCreationDto object is null");

        var clockworkToReturn = await _service.ClockworkTasks.CreateClockworkTaskAsync(accountId, clockworkTask, false);
        return CreatedAtRoute("GetClockworkForAccount", new {
            accountId,
            id = clockworkToReturn.Id
        }, clockworkToReturn);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteClockworkTask(Guid accountId, Guid id) {
        await _service.ClockworkTasks.DeleteClockworkTaskAsync(accountId, id, false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateClockworkTask(Guid accountId, Guid id, [FromBody] ClockworkTaskForUpdateDto? clockworkTaskForUpdateDto) {
        if (clockworkTaskForUpdateDto is null)
            return BadRequest("ClockworkTaskForUpdateDto object is null");

        await _service.ClockworkTasks.UpdateClockworkTaskAsync(accountId, id, clockworkTaskForUpdateDto, false, true);

        return NoContent();
    }
}