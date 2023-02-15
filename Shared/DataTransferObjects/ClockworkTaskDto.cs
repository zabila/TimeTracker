namespace Shared.DataTransferObjects;

public record ClockworkTaskDto {
    public Guid Id { get; init; }
    public int ClockworkTaskId { get; init; }
    public string? ClockworkTaskKey { get; init; }
    public DateTime StartedDateTime { get; init; }
    public TimeSpan TimeSpentSeconds { get; init; }
    public Guid AccountId { get; set; }
};