using ClientsAPI.Contracts;
using ClientsAPI.Persistency;
using ClientsAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ClientsDbContext>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "ClientsAPI");

app.Run();
