using ResourcesAPI.Services;
using ResourcesAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
ConfigureServices(builder.Services);
var app = builder.Build();

app.MapGet("/", () => "ResourcesAPI");

app.MapControllers();
app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IResourcesService, ResourcesService>();
}
