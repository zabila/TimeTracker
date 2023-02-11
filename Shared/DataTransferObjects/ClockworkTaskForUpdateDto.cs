namespace Shared.DataTransferObjects;

public record ClockworkTaskForUpdateDto
{
    public DateTime StartedDateTime { get; init; }
    public TimeSpan TimeSpentSeconds { get; init; }
}