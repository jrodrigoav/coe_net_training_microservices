using ClientsAPI.Controllers;
using ClientsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add a service to DI
builder.Services.AddSingleton<IClientService, ClientService>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ClientsController).Assembly)
    .AddControllersAsServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "ClientsAPI");

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
