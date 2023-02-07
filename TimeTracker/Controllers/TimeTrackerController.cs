using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace TimeTracker.Controllers;

[Route("[controller]")]
[ApiController]
public class TimeTrackerController : ControllerBase
{
    private readonly ILoggerManager _logger;
    private readonly IRepositoryManager _repository;

    TimeTrackerController(ILoggerManager logger, IRepositoryManager repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        _logger.LogInfo("Here is info message from our values controller.");
        _logger.LogDebug("Here is debug message from our values controller.");
        _logger.LogError("Here is error message from our values controller.");
        _logger.LogWarn("Here is warn message from our values controller.");

        return new string[] { "value1", "value2" };
    }
}