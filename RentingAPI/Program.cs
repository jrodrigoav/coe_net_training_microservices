using RentingAPI.Models;
using RentingAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<RentingService>();

builder.Services.AddHttpClient<ClientApi>().ConfigureHttpClient(client =>
{
    var baseAddress = builder.Configuration.GetSection("APIs")["Client"] ?? throw new NullReferenceException("Missing 'APIs__Client' configuration.");
    client.BaseAddress = new Uri(baseAddress);
});
builder.Services.AddHttpClient<InventoryApi>().ConfigureHttpClient(client =>
{
    var baseAddress = builder.Configuration.GetSection("APIs")["Inventory"] ?? throw new NullReferenceException("Missing 'APIs__Inventory' configuration.");
    client.BaseAddress = new Uri(baseAddress);
});
builder.Services.AddHttpClient<ResourceApi>().ConfigureHttpClient(client =>
{
    var baseAddress = builder.Configuration.GetSection("APIs")["Resource"] ?? throw new NullReferenceException("Missing 'APIs__Resource' configuration.");
    client.BaseAddress = new Uri(baseAddress);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
