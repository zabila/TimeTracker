namespace Shared.RequestFeatures;

public class ClockworkTasksParameters : RequestParameters {
    
    public DateTime FromStartedDateTime { get; set; }
    public DateTime ToStartedDateTime { get; set; } = DateTime.MaxValue;
    public TimeSpan Duration { get; set; }
    
    public string? SearchTerm { get; set; }

    public bool ValidStartedDateTimeRange {
        get {
            return ToStartedDateTime > FromStartedDateTime;
        }
    }
    
    public bool IsStartedTask {
        get {
            return Duration == TimeSpan.Zero;
        }
    }
}