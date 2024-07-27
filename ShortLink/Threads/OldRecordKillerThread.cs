using Microsoft.EntityFrameworkCore;
using ShortLink.Database;

namespace ShortLink.Threads
{
    public class OldRecordKillerThread(ILogger<OldRecordKillerThread> logger, MainDb mainDb) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var cutTime = DateTime.Now;
                logger.LogInformation($"Worker running at: [{cutTime}]");
                var deleted = await mainDb.Links.Where(x => x.KillTime < cutTime).ExecuteDeleteAsync(cancellationToken: stoppingToken);
                if (deleted != 0) logger.LogInformation($"Deleted [{deleted}] records");
                await Task.Delay(1000 * 60, stoppingToken);
            }
        }
    }
}
