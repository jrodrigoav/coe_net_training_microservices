using Amazon.DynamoDBv2.DataModel;
using ResourcesAPI.Controllers;
using ResourcesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add a service to DI
builder.Services.AddSingleton<IDynamoDbService, MockDynamoDbService>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ResourcesController).Assembly)
    .AddControllersAsServices();


var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "ResourcesAPI");

app.MapControllers();

app.Run();
