using RentingAPI.Repository;
using RentingAPI.Repository.Interface;
using RentingAPI.Services;
using RentingAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
ConfigureServices(builder.Services);

var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "RentingAPI");

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IRentingService, RentingService>();
    services.AddTransient<IDynamoRepository, DynamoRepository>();
}
