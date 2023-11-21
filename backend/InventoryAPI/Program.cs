using InventoryAPI.Services;
using InventoryAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
ConfigureServices(builder.Services);

var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "InventoryAPI");

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IInventoryService, InventoryService>();
}