namespace Entities.Exceptions;

public class ClockworkTaskNotFoundException : NotFoundException
{
    public ClockworkTaskNotFoundException(Guid clockworkTaskId)
        : base($"ClockworkTask with id: {clockworkTaskId} was not found.")
    {
    }
}