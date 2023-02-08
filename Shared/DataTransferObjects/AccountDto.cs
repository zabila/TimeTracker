namespace Shared.DataTransferObjects;

public record AccountDto(
    Guid Id, 
    string UserName,
    string FirstName,
    string LastName,
    int Type);