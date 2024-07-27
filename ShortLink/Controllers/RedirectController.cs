using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLink.Database;

namespace ShortLink.Controllers;
[ApiController]
[Route("r/{id}")]
public class RedirectController(MainDb mainDb) : ControllerBase
{
    public async Task<IActionResult> Get(string id)
    {
        var link = await mainDb.Links.SingleOrDefaultAsync(x => x.UrlShort == id);
        return link == null ? NotFound() : Redirect(link.UrlLong);
    }
}