using InventoryAPI.Models;
using InventoryAPI.Services;
using InventoryAPI.Services.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    builder.Services.AddDbContext<InventoryDbContext>(optionsAction => optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("MicroservicesDB")));

    builder.Services.Configure<ResourceAPISettings>(builder.Configuration.GetSection(nameof(ResourceAPISettings)));

    builder.Services.AddHttpClient<ResourceAPIClient>();
}
var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapGet("/", () => "InventoryAPI");
    app.MapGet("/lbhealth", () => "InventoryAPI");
    app.MapControllers();
}
app.Run();
