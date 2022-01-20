using Microsoft.AspNetCore.ResponseCompression;
using Realms;
using src.Respositories;
using src.Respositories.Infra.Databases.RealmDB;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(typeof(IRepository<>), typeof(RealmDatabase<RealmObject>));
builder.Services.AddResponseCompression();
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseResponseCompression();

app.Run();
