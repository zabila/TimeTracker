using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Contracts;

public interface IClockworkLinks {
    LinkResponse TryGenerateLinks(IEnumerable<ClockworkTaskDto> clockworkTaskDtos, string fieldsString, Guid accountId, HttpContext httpContext);
}