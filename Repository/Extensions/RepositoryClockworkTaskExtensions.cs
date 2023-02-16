using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Entities.Models;
using Repository.Extensions.Utility;

namespace Repository.Extensions;

public static class RepositoryClockworkTaskExtensions {
    public static IQueryable<ClockworkTask> Filter(this IQueryable<ClockworkTask> tasks, DateTime fromStartedDateTime, DateTime toStartedDateTime) {
        return tasks.Where(t => t.StartedDateTime >= fromStartedDateTime && t.StartedDateTime <= toStartedDateTime);
    }

    public static IQueryable<ClockworkTask> Search(this IQueryable<ClockworkTask> tasks, string? searchTerm) {

        if (string.IsNullOrWhiteSpace(searchTerm)) {
            return tasks;
        }

        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        return tasks.Where(t => t.ClockworkTaskKey.ToLower().Contains(lowerCaseSearchTerm));
    }

    public static IQueryable<ClockworkTask> Sort(this IQueryable<ClockworkTask> tasks, string orderByQueryString) {

        if (string.IsNullOrWhiteSpace(orderByQueryString)) {
            return tasks.OrderBy(t => t.ClockworkTaskKey);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<ClockworkTask>(orderByQueryString);

        return string.IsNullOrWhiteSpace(orderQuery) ? tasks.OrderBy(t => t.ClockworkTaskKey) : tasks.OrderBy<ClockworkTask>(orderQuery);

    }
}