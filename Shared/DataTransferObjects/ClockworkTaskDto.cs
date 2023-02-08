namespace Shared.DataTransferObjects;

public record ClockworkTaskDto(
    Guid Id,
    int ClockworkTaskId,
    string ClockworkTaskKey,
    DateTime StartedDateTime,
    TimeSpan TimeSpentSeconds
);