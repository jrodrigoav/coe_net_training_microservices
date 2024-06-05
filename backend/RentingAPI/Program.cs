
using Microsoft.EntityFrameworkCore;
using RentingAPI.Models;
using RentingAPI.Services;
using RentingAPI.Services.Data;
const string DEFAULT_CORS_POLICY = "AllowLocalhostCORSPolicy";
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
    builder.Services.Configure<ResourcesAPISettings>(builder.Configuration.GetSection(nameof(InventoryAPISettings)));

    builder.Services.AddHttpClient<ClientAPIClient>();
    builder.Services.AddHttpClient<InventoryAPIClient>();
    builder.Services.AddHttpClient<ResourcesAPISettings>();
    builder.Services.AddScoped<InventoryAPIClient>();
    builder.Services.AddScoped<ResourcesAPIClient>();

    builder.Services.AddDbContext<RentingDbContext>(optionsAction => optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("MicroservicesDB")));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: DEFAULT_CORS_POLICY,
            policy =>
            {
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                policy.WithOrigins("http://localhost:4200");
            });
    });
}
var app = builder.Build();
{
    app.UseCors(DEFAULT_CORS_POLICY);
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
