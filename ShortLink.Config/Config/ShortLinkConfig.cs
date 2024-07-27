namespace ShortLink.Config.Config;

public class ShortLinkConfig
{
    public static ShortLinkConfig CreateDefault() => new()
    { BaseUrl = string.Empty, DbConnection = string.Empty, MaxTimespan = 0 };

    public required string BaseUrl { get; init; }
    public ulong MaxTimespan { get; init; }
    public required string DbConnection { get; init; }
}