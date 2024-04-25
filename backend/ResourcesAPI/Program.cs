using Microsoft.EntityFrameworkCore;
using ResourcesAPI.Services.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    builder.Services.AddDbContext<ResourcesDbContext>(optionsAction => optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("MicroservicesDB")));
}
var app = builder.Build();
{    
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