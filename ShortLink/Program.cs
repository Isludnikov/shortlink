using ShortLink.Config.Config;
using ShortLink.Database;
using ShortLink.Threads;

var configuration = ReadConfiguration();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddConfiguration(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(configuration.GetSection("Shortlink").Get<ShortLinkConfig>() ?? ShortLinkConfig.CreateDefault());
builder.Services.AddTransient<MainDb>();
builder.Services.AddHostedService<OldRecordKillerThread>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseAuthorization();

app.MapControllers();

app.Run();
static IConfiguration ReadConfiguration()
{
    Console.WriteLine("Чтение конфигурации");

    var baseFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
    var path = Path.Combine(baseFolder, "Config");

    var configurationBuilder = new ConfigurationBuilder();
    foreach (var jsonFileName in Directory.EnumerateFiles(path, "*.json", SearchOption.AllDirectories))
    {
        Console.WriteLine($"Добавление конфига {jsonFileName}");
        configurationBuilder.AddJsonFile(jsonFileName);
        Console.WriteLine($"Добавлен конфиг {jsonFileName}");
    }

    return configurationBuilder.Build();
}