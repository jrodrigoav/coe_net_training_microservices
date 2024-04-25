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
}
var app = builder.Build();
{    
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
