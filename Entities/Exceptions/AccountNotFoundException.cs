namespace Entities.Exceptions;

public sealed class AccountNotFoundException : NotFoundException
{
    public AccountNotFoundException(int accountId)
        : base($"Account with id: {accountId} was not found.")
    {
    }
}