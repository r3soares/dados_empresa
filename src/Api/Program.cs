using Microsoft.AspNetCore.ResponseCompression;
using Realms;
using src.Respositories;
using src.Respositories.Infra.Databases.RealmDB;
using System.IO.Compression;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(typeof(IRepository<>), typeof(RealmDatabase<>));
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
}
);
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseResponseCompression();

app.Run();
