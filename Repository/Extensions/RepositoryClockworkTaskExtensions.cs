using Entities.Models;

namespace Repository.Extensions; 

public static class RepositoryClockworkTaskExtensions {
    public static IQueryable<ClockworkTask> FilterClockworkTasks (this IQueryable<ClockworkTask> tasks, DateTime fromStartedDateTime, DateTime toStartedDateTime) {
        return tasks.Where(t => t.StartedDateTime >= fromStartedDateTime && t.StartedDateTime <= toStartedDateTime);
    }
    
    public static IQueryable<ClockworkTask> SearchClockworkTasks (this IQueryable<ClockworkTask> tasks, string? searchTerm) {
        
        if (string.IsNullOrWhiteSpace(searchTerm)) {
            return tasks;
        }

        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        return tasks.Where(t => t.ClockworkTaskKey.ToLower().Contains(lowerCaseSearchTerm));
    }
}