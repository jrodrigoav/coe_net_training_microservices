using Microsoft.EntityFrameworkCore;
using ResourcesAPI.Services.Data;

const string DEFAULT_CORS_POLICY = "AllowLocalhostCORSPolicy";
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
    //public class ResourcesController(ResourcesDbContext resourcesDbContext, [FromServices] RentingDbContext rentingDbContext) : ControllerBase

    builder.Services.AddDbContext<ResourcesDbContext>(optionsAction => optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("MicroservicesDB")));
    //builder.Services.AddDbContext<RentingDbContext>(optionsAction => optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("MicroservicesDB")));

    //builder.Services.AddScoped<RentingDbContext>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: DEFAULT_CORS_POLICY,
            policy =>
            {
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                policy.WithOrigins( "http://localhost:4200", 
                                    "http://localhost:5183", 
                                    "http://localhost:5173",
                                    "http://localhost:5181");
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
    app.MapGet("/", () => "ResourcesAPI");
    app.MapGet("/lbhealth", () => "ResourcesAPI");
    app.MapControllers();
}
app.Run();