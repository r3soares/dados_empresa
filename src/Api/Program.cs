using Realms;
using src.Respositories;
using src.Respositories.Infra.Databases.RealmDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(typeof(IRepository<>), typeof(RealmDatabase<RealmObject>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
