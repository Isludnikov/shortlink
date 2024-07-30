using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLink.Database;

namespace ShortLink.Controllers;
[ApiController]
[Route("r/{id}")]
public class RedirectController(ILogger<RedirectController> logger, MainDb mainDb) : ControllerBase
{
    public async Task<IActionResult> Get(string id)
    {
        var link = await mainDb.Links.SingleOrDefaultAsync(x => x.UrlShort == id);
        if (link == null)
        {
            logger.LogDebug($"Url [{id}] not found");
            return NotFound();
        }
        logger.LogDebug($"Url [{id}] found, redirecting...");
        return Redirect(link.UrlLong);
    }
}