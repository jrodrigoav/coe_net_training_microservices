
var builder = WebApplication.CreateBuilder(args);

var configuration = context.Configuration;
services.Configure<DynamoDbSettings>(configuration.GetSection("DynamoDBSettings"));

var app = builder.Build();

app.MapGet("/", () => "RentingAPI");

app.Run();
