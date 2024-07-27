using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShortLink.Config.Config;

namespace ShortLink.Database;

public class MainDbDesignTimeFactory : IDesignTimeDbContextFactory<MainDb>
{
    private const string ConnectionStringDesignTime = "server=192.168.33.10;port=3306;user id=user;password=password;database=link_compactor;persistsecurityinfo=True;charset=utf8";
    public MainDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MainDb>();
        optionsBuilder.UseMySql(ConnectionStringDesignTime, ServerVersion.AutoDetect(ConnectionStringDesignTime), options => options.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(3),
            errorNumbersToAdd: null
        ));

        return new MainDb(new ShortLinkConfig { BaseUrl = string.Empty, DbConnection = ConnectionStringDesignTime, MaxTimespan = 0 });
    }
}