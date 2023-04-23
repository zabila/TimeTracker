using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using TimeTracker.Presentation.ActionFilters;

namespace TimeTracker.Presentation.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase {
    private readonly IServiceManager _service;

    public TokenController(IServiceManager service) {
        _service = service;
    }

    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto) {
        var tokenDtoToReturn = await _service.Authentication.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}