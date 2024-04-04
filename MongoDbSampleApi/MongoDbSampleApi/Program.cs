using MongoDB.Driver;
using MongoDbSampleApi.Interfaces;
using MongoDbSampleApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddTransient<IProduct, ProductRepository>();
builder.Services.AddTransient<ICustomer, CustomerRepository>();

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
