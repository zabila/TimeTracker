using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace TimeTracker.Presentation.Controllers;

[Route("api")]
[ApiController]
public class RootController : ControllerBase {
    private readonly LinkGenerator _linkGenerator;

    public RootController(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;


    [HttpGet(Name = "GetRoot")]
    public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType) {
        if (mediaType.Contains("application/vnd.marvel.apiroot+json")) {
            var list = new List<Link> {
                new Link {
                    Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new {
                    }),
                    Rel = "self",
                    Method = "GET"
                },
                new Link {
                    Href = _linkGenerator.GetUriByName(HttpContext, nameof(AccountsController.GetAccounts), new {
                    }),
                    Rel = "accounts",
                    Method = "GET"
                },
                new Link {
                    Href = _linkGenerator.GetUriByName(HttpContext, nameof(AccountsController.CreateAccount), new {
                    }),
                    Rel = "create_account",
                    Method = "POST"
                }
            };
            return Ok(list);
        }
        return NoContent();
    }
}