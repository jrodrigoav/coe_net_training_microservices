using RentingAPI.Contracts;
using RentingAPI.Persistency;
using RentingAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("Clients", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Client"]!);
});

builder.Services.AddHttpClient("Inventory", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Inventory"]!);
});

builder.Services.AddHttpClient("Resources", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Resources"]!);
});

builder.Services.AddSingleton<RentingDbContext>();
builder.Services.AddScoped<IRentingService, RentingService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "RentingAPI");

app.Run();
