using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using TimeTracker.Presentation.ActionFilters;

namespace TimeTracker.Presentation.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase {
    private readonly IServiceManager? _service;

    public AuthenticationController(IServiceManager service) {
        _service = service;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration) {
        var result = await _service!.Authentication.RegisterUser(userForRegistration);
        if (!result.Succeeded) {
            foreach (var error in result.Errors) {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }
        return StatusCode(201);
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto
        user) {
        if (!await _service!.Authentication.ValidateUser(user))
            return Unauthorized();

        var tokenDto = await _service.Authentication.CreateToken(true);

        return Ok(tokenDto);
    }
}