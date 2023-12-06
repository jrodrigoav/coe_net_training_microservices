using InventoryAPI.Controllers;
using InventoryAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add a service to DI
builder.Services.AddSingleton<IInventoryService, InventoryService>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(InventoryController).Assembly)
    .AddControllersAsServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "InventoryAPI");

app.MapControllers();

app.Run();
