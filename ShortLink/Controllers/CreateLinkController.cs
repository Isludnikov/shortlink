using Microsoft.AspNetCore.Mvc;
using ShortLink.Database;
using ShortLink.Database.Entities;
using ShortLink.Helpers;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ShortLink.Attributes;
using ShortLink.Config.Config;

namespace ShortLink.Controllers;
[ApiController]
[Route("create")]
public class CreateLinkController(ILogger<CreateLinkController> logger, ShortLinkConfig config) : ControllerBase
{
    [HttpPost(Name = "CreateLink")]
    public async Task<CreateLinkResponse> Post(CreateLinkRequest request)
    {
        await using var db = new MainDb(config);
        var existingElement = await db.Links.FirstOrDefaultAsync(x => x.UrlLong == request.Url);
        if (existingElement != null)
        {
            logger.LogDebug($"Url [{request.Url}] already exists");
            return new CreateLinkResponse { EndTime = existingElement.KillTime, Url = existingElement.UrlShort };
        }

        var newLink = new Link
        {
            KillTime = DateTime.Now + (request.Ttl != null ? TimeSpan.FromSeconds(ulong.Min(config.MaxTimespan, request.Ttl.Value)) : TimeSpan.FromSeconds(config.MaxTimespan)),
            UrlLong = request.Url,
            UrlShort = CryptoHelper.GetRandomString()
        };
        db.Links.Add(newLink);
        await db.SaveChangesAsync();
        logger.LogDebug($"New item added Id [{newLink.Id}] Url [{newLink.UrlLong}]");
        return new CreateLinkResponse { EndTime = newLink.KillTime, Url = config.BaseUrl + newLink.UrlShort };
    }
}

public class CreateLinkResponse
{
    public required string Url { get; init; }
    public DateTime EndTime { get; init; }
}

public class CreateLinkRequest
{
    [Required]
    [StringLength(3000)]
    [UrlValid]
    [IsLatin]
    public required string Url { get; init; }
    public ulong? Ttl { get; init; }
}