namespace Shared.DataTransferObjects;

public record AccountDto {
    public Guid Id { get; init; }
    public string? UserName { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public int Type { get; init; }
}