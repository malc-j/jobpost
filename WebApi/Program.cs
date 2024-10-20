using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Serilog;
using System.Reflection.Metadata;
using WebApi.Entities.Context;
using WebApi.Services.Repositories;


var builder = WebApplication.CreateBuilder(args);
// Configure SeriLog and create logger factory for dependency injection
//var loggerConfiguration = new ConfigurationBuilder().AddJsonFile("./appsettings.json").Build();
//var seriLogger = new LoggerConfiguration().ReadFrom.Configuration(loggerConfiguration).CreateLogger();
//ILoggerFactory loggingFactory = new LoggerFactory().AddSerilog(seriLogger);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddSingleton(loggingFactory);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Application starting up....", DateTime.UtcNow);
    Log.Information("Application started running {0}", DateTime.UtcNow); 
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application failed to run! {0}", DateTime.UtcNow);
}
finally
{
    Log.CloseAndFlush();
}
