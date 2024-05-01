
using RentingAPI.Models;
using RentingAPI.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    builder.Services.Configure<ClientsAPISettings>(builder.Configuration.GetSection(nameof(ClientsAPISettings)));
    builder.Services.Configure<InventoryAPISettings>(builder.Configuration.GetSection(nameof(InventoryAPISettings)));
    builder.Services.Configure<ResourcesAPISettings>(builder.Configuration.GetSection(nameof(ResourcesAPISettings)));

    builder.Services.AddHttpClient<ClientAPIClient>();
    builder.Services.AddHttpClient<InventoryAPIClient>();
    builder.Services.AddHttpClient<ResourcesAPISettings>();
}
var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapGet("/", () => "RentingAPI");
    app.MapGet("/lbhealth", () => "RentingAPI");
    app.MapControllers();
}
app.Run();
