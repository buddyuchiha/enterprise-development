using AutoMapper;
using AviaCompany.Application.Contracts;
using AviaCompany.Application.Contracts.AircraftFamily;
using AviaCompany.Application.Contracts.AircraftModel;
using AviaCompany.Application.Contracts.Flight;
using AviaCompany.Application.Contracts.Passenger;
using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Application.Mappings;
using AviaCompany.Application.Services;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Aircrafts;
using AviaCompany.Domain.Models.Flights;
using AviaCompany.Domain.Models.Passengers;
using AviaCompany.Domain.Models.Tickets;
using AviaCompany.Infrastructure.EfCore;
using AviaCompany.Infrastructure.EfCore.Repositories;
using AviaCompany.ServiceDefaults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFiles = new[]
    {
        "AviaCompany.WebApi.xml",
        "AviaCompany.Domain.xml",
        "AviaCompany.Application.xml",
        "AviaCompany.Application.Contracts.xml"
    };

    foreach (var xmlFile in xmlFiles)
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new AviaCompanyProfile());
});

builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<IAircraftFamilyService, AircraftFamilyService>();
builder.Services.AddScoped<IAircraftModelService, AircraftModelService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddScoped<IApplicationService<FlightDto, FlightCreateUpdateDto, int>, FlightService>();
builder.Services.AddScoped<IApplicationService<PassengerDto, PassengerCreateUpdateDto, int>, PassengerService>();
builder.Services.AddScoped<IApplicationService<TicketDto, TicketCreateUpdateDto, int>, TicketService>();
builder.Services.AddScoped<IApplicationService<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, int>, AircraftFamilyService>();
builder.Services.AddScoped<IApplicationService<AircraftModelDto, AircraftModelCreateUpdateDto, int>, AircraftModelService>();

builder.Services.AddScoped<IRepository<Flight, int>, FlightEfCoreRepository>();
builder.Services.AddScoped<IRepository<AircraftFamily, int>, AircraftFamilyEfCoreRepository>();
builder.Services.AddScoped<IRepository<AircraftModel, int>, AircraftModelEfCoreRepository>();
builder.Services.AddScoped<IRepository<Passenger, int>, PassengerEfCoreRepository>();
builder.Services.AddScoped<IRepository<Ticket, int>, TicketEfCoreRepository>();

builder.AddMySqlDbContext<AviaCompanyDbContext>(connectionName: "DefaultConnection");
builder.AddServiceDefaults();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AviaCompanyDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();