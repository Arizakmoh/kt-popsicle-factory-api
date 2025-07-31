using FluentValidation;
using Mapster;
using MapsterMapper;
using PopsicleFactory.Application.Validators;
using PopsicleFactory.Domain.Interfaces;
using PopsicleFactory.Infrastructure.Data;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// services to the container
//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddValidatorsFromAssemblyContaining<CreatePopsicleDtoValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection Configure
builder.Services.AddSingleton<IPopsicleRepository, InMemoryDbPopsicleRepository>();

// Configure Mapster for my custom mapping
var config = TypeAdapterConfig.GlobalSettings;
// custom mapping
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

var app = builder.Build();

// Check donet Version 
Console.WriteLine($"Framework: {RuntimeInformation.FrameworkDescription}");


// Configure the HTTP request for pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
// H
