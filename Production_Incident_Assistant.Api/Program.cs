using Microsoft.EntityFrameworkCore;
using Production_Incident_Assistant.Core.Interfaces;
using Production_Incident_Assistant.Core.Services;
using Production_Incident_Assistant.Infrastructure.Data;
using Production_Incident_Assistant.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IncidentDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Production_Incident_Assistant.Api")));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();
});

builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IncidentService>();

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
