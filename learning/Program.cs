using FluentValidation;
using learning.Data;
using learning.Filters;
using learning.Models;
using learning.Validators;
using Mapster;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

IConfigurationBuilder configBuilder = builder.Configuration;
configBuilder.Sources.Clear();  
configBuilder.AddJsonFile("appsettings.json")  
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .AddEnvironmentVariables()
    .AddCommandLine(args);

// Add services to the container.

builder.Services.AddDbContext<BillingContext>();
builder.Services.AddScoped<IBillingRepository, BillingRepository>();

builder.Services.AddControllers();
builder.Services.AddScoped<IValidator<TimeBillModel>, TimeBillModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AbstractValidator<TimeBillModel>>();
builder.Services.AddScoped<ValidateModelFilter<TimeBillModel>>();

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly()!);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
