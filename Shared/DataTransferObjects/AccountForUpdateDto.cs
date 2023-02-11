using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AccountForUpdateDto
{
    [MaxLength(30, ErrorMessage = "Account name cannot be longer than 30 characters")]
    public string? UserName { get; init; }

    public string? ClockworkAccountId { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Password { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Type is required and it can't be lower than 1")]
    public int Type { get; init; }

    public string? AuthorizationToken { get; init; }
    public IEnumerable<ClockworkTaskForCreationDto>? Tasks { get; init; }
}