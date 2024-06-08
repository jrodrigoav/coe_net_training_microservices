using ClientsAPI.Services.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    if (builder.Environment.IsDevelopment())
    { 
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
    
    builder.Services.AddDbContext<ClientsDbContext>(optionsAction => optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("MicroservicesDB")));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: "AllowLocalhostCORSPolicy",
            policy =>
            {
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                policy.AllowAnyOrigin();
                policy.DisallowCredentials();

                policy.WithOrigins("https://localhost:7122", 
                                    "https://localhost:4200",
                                    "http://localhost:4200", 
                                    "https://localhost:5183", 
                                    "http://localhost:5183", 
                                    "http://localhost:5180",
                                    "http://localhost:5173");
            });
    });
}
var app = builder.Build();
{
    app.UseCors("AllowLocalhostCORSPolicy");
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.MapGet("/", () => "ClientsAPI");
    app.MapGet("/lbhealth", () => "ClientsAPI");
    app.MapControllers();
}
app.Run();
