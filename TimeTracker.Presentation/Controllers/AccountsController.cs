using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace TimeTracker.Presentation.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IServiceManager _service;

    public AccountsController(IServiceManager serviceManager) => _service = serviceManager;

    [HttpGet]
    public IActionResult GetAccounts()
    {
        var accounts = _service.Accounts.GetAllAccounts(false);
        return Ok(accounts);
    }

    [HttpGet("{id:Guid}", Name = "AccountById")]
    public IActionResult GetAccount(Guid id)
    {
        var account = _service.Accounts.GetAccount(id, false);
        if (account is null)
            throw new AccountNotFoundException(id);

        return Ok(account);
    }

    [HttpPost]
    public IActionResult CreateAccount([FromBody] AccountForCreationDto? account)
    {
        if (account is null)
            return BadRequest("AccountForCreationDto object is null");

        var accountEntity = _service.Accounts.CreateAccount(account);
        return CreatedAtRoute("AccountById", new { id = accountEntity.Id }, accountEntity);
    }
}