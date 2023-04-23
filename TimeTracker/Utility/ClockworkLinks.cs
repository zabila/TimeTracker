using System.Diagnostics.CodeAnalysis;
using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;

namespace TimeTracker.Utility;

public class ClockworkLinks : IClockworkLinks {

    private readonly LinkGenerator _linkGenerator;
    private readonly IDataShaper<ClockworkTaskDto> _dataShaper;

    public ClockworkLinks(LinkGenerator linkGenerator, IDataShaper<ClockworkTaskDto> dataShaper) {
        _linkGenerator = linkGenerator;
        _dataShaper = dataShaper;
    }

    public LinkResponse TryGenerateLinks(IEnumerable<ClockworkTaskDto> clockworkTaskDtos, string fieldsString, Guid accountId, HttpContext httpContext) {
        var shapedClockworkTasks = ShapeData(clockworkTaskDtos, fieldsString);

        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkedClockworkTasks(clockworkTaskDtos, fieldsString, accountId, httpContext, shapedClockworkTasks);

        return ReturnShapedClockworkTasks(shapedClockworkTasks);

    }

    private LinkResponse ReturnShapedClockworkTasks(List<Entity> shapedClockworkTasks) {
        return new LinkResponse {
            ShapeEntities = shapedClockworkTasks
        };
    }

    private List<Entity> ShapeData(IEnumerable<ClockworkTaskDto> clockworkTaskDtos, string fieldsString) {
        return _dataShaper.ShapeData(clockworkTaskDtos, fieldsString)
            .Select(e => e.Entity)
            .ToList();
    }

    private bool ShouldGenerateLinks(HttpContext httpContext) {
        var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"]!;
        return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }

    private LinkResponse ReturnLinkedClockworkTasks(IEnumerable<ClockworkTaskDto> clockworkTaskDtos, string fields, Guid accountId, HttpContext httpContext, List<Entity> shapedClockworkTasks) {
        var clockworkTasksList = clockworkTaskDtos.ToList();

        for (var index = 0; index < clockworkTasksList.Count; index++) {
            var clockworkTaskLinks = CreateLinksForClockworkTask(httpContext, accountId, clockworkTasksList[index].Id, fields);
            shapedClockworkTasks[index].TryAdd("Links", clockworkTaskLinks);
        }

        var clockworkTasksResource = new LinkCollectionWrapper<Entity>(shapedClockworkTasks);
        var linkedClockworkTasks = CreateLinksForClockworkTasks(httpContext, clockworkTasksResource);

        return new LinkResponse {
            HasLinks = true,
            LinkedEntities = linkedClockworkTasks
        };
    }

    private List<Link> CreateLinksForClockworkTask(HttpContext httpContext, Guid accountId, Guid id, string fields = "") {
        var links = new List<Link> {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetClockworkTasksForAccount",
                    values: new {
                        accountId = accountId,
                        id,
                        fields
                    }),
                "self",
                "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext,
                    "DeleteClockworkTask", values: new {
                        accountId = accountId,
                        id
                    }),
                "delete_employee",
                "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext,
                    "UpdateClockworkTask", values: new {
                        accountId = accountId,
                        id
                    }),
                "update_employee",
                "PUT")
        };
        return links;
    }

    private LinkCollectionWrapper<Entity> CreateLinksForClockworkTasks(HttpContext httpContext, LinkCollectionWrapper<Entity> clockworkTasksResource) {

        clockworkTasksResource.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext,
                "GetClockworkTasksForAccount", values: new {
                }),
            "self",
            "GET"));
        return clockworkTasksResource;
    }
}