using InventoryAPI.Models;
using InventoryAPI.Services;
using InventoryAPI.Services.Data;
using Microsoft.EntityFrameworkCore;
const string DEFAULT_CORS_POLICY = "AllowLocalhostCORSPolicy";
var builder = WebApplication.CreateBuilder(args);
{
 

    builder.Services.AddDbContext<InventoryDbContext>(optionsAction => optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("MicroservicesDB")));

    builder.Services.Configure<ResourceAPISettings>(builder.Configuration.GetSection(nameof(ResourceAPISettings)));

    builder.Services.AddHttpClient<ResourceAPIClient>();

    builder.Services.AddControllers();
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    builder.Services.AddScoped<InventoryService>();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: DEFAULT_CORS_POLICY,
            policy =>
            {
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();

                policy.WithOrigins("http://localhost:4200", 
                                    "http://localhost:5181", 
                                    "http://localhost:5173", 
                                    "http://localhost:5183",
                                    "http://localhost:5182");


            });
    });

}
var app = builder.Build();
{
    app.UseCors(DEFAULT_CORS_POLICY);
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
