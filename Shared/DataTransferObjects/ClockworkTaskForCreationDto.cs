namespace Shared.DataTransferObjects;

public record ClockworkTaskForCreationDto {
    public int ClockworkTaskId { get; init; }
    public string? ClockworkTaskKey { get; init; }
    public DateTime StartedDateTime { get; init; }
    public TimeSpan TimeSpentSeconds { get; init; }
};