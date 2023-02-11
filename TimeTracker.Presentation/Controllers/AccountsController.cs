﻿using Entities.Exceptions;
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
    public async Task<IActionResult> GetAccounts()
    {
        var accounts = await _service.Accounts.GetAllAccountsAsync(false);
        return Ok(accounts);
    }

    [HttpGet("{id:Guid}", Name = "AccountById")]
    public async Task<IActionResult> GetAccount(Guid id)
    {
        var account = await _service.Accounts.GetAccountAsync(id, false);
        if (account is null)
            throw new AccountNotFoundException(id);

        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] AccountForCreationDto? account)
    {
        if (account is null)
            return BadRequest("AccountForCreationDto object is null");

        var accountEntity = await _service.Accounts.CreateAccountAsync(account);
        return CreatedAtRoute("AccountById", new { id = accountEntity.Id }, accountEntity);
    }

    [HttpDelete("{accountId:guid}")]
    public async Task<IActionResult> DeleteAccount(Guid accountId)
    {
        await _service.Accounts.DeleteAccountAsync(accountId);
        return NoContent();
    }

    [HttpPut("{accountId:guid}")]
    public async Task<IActionResult> UpdateAccount(Guid accountId, AccountForUpdateDto accountForUpdateDto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.Accounts.UpdateAccountAsync(accountId, accountForUpdateDto, true);
        return NoContent();
    }
}