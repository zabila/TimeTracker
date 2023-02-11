namespace Shared.DataTransferObjects;

public record AccountForCreationDto
{
    public string? UserName { get; init; }
    public string? ClockworkAccountId { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Password { get; init; }
    public int Type { get; init; }
    public string? AuthorizationToken { get; init; }
    public IEnumerable<ClockworkTaskForCreationDto>? Tasks { get; init; }
}