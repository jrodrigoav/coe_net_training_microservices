using InventoryAPI.Contracts;
using InventoryAPI.Helpers;
using InventoryAPI.Persistency;
using InventoryAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<InventoryDbContext>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IHttpRespondeReader, HttpResponseReader>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "InventoryAPI");

app.Run();
