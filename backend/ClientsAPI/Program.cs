using ClientsAPI.Models;
using ClientsAPI.Services.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    if (builder.Environment.IsDevelopment())
    { 
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
    // Database
    builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
    builder.Services.AddScoped<ClientsService>();
}
var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.MapControllers();
}
app.Run();
