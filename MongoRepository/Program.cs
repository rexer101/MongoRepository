using MongoDB.Driver;
using MongoRepository.Implementation;
using MongoRepository.Infrastructure.Interfaces;
using MongoRepository.Infrastructure.Repositories;
using MongoRepository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient("mongodb://sa:Password123@127.0.10.1:27017,127.0.10.2:27017,127.0.10.3:27017/myDatabase?authSource=admin"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
