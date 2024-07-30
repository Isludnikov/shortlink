using Microsoft.EntityFrameworkCore;
using ShortLink.Config.Config;
using ShortLink.Database.Entities;

namespace ShortLink.Database;

public class MainDb(ShortLinkConfig config) : DbContext
{
    private string ConnectionString { get; set; } = config.DbConnection;
    public DbSet<Link> Links { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionStr = ConnectionString;
            optionsBuilder.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr), options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(3),
                errorNumbersToAdd: null
            ));

            /*optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new MainDbLoggerProvider(logger));
            }));*/
        }
    }
}