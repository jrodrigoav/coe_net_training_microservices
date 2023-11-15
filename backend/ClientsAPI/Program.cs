using ClientsAPI.Services;
using ClientsAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
ConfigureServices(builder.Services);

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => "ClientsAPI");

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddHttpClient();
    services.AddSingleton<IClientService, ClientService>();
}
