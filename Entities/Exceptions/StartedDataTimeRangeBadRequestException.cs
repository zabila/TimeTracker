namespace Entities.Exceptions;

public class StartedDataTimeRangeBadRequestException : BadRequestException {
    public StartedDataTimeRangeBadRequestException(DateTime fromStartedDateTime, DateTime toStartedDateTime) :
        base("The 'FromStartedDateTime' must be less than 'ToStartedDateTime'. " +
             $"FromStartedDateTime: {fromStartedDateTime}, ToStartedDateTime: {toStartedDateTime}") {
    }
}