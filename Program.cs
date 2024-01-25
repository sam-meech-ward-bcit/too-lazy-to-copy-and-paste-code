using Microsoft.EntityFrameworkCore;
using MyApp.Models;

DotNetEnv.Env.Load();

// var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

var PGHOST = Environment.GetEnvironmentVariable("PGHOST");
var PGDATABASE = Environment.GetEnvironmentVariable("PGDATABASE");
var PGUSER = Environment.GetEnvironmentVariable("PGUSER");
var PGPASSWORD = Environment.GetEnvironmentVariable("PGPASSWORD");
var connectionString = $"Host={PGHOST};Database={PGDATABASE};Username={PGUSER};Password={PGPASSWORD}";


var builder = WebApplication.CreateBuilder(args);

// var connectionString = "Host=ep-nameless-resonance-a6vkgi4i.us-west-2.aws.neon.tech;Database=neondb;Username=meech-ward;Password=5yQDMIT8UgXp";
// var connectionString = "Host=127.0.0.1;Database=my_dumb_app;Username=my_dumb_app_role;Password=some_password";
builder.Services.AddDbContext<DatabaseContext>(
    opt =>
    {
      opt.UseNpgsql(connectionString);
      if (builder.Environment.IsDevelopment())
      {
        opt
          .LogTo(Console.WriteLine, LogLevel.Information)
          .EnableSensitiveDataLogging()
          .EnableDetailedErrors();
      }
    }
);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
