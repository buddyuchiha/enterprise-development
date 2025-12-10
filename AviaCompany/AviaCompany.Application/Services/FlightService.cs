using AutoMapper;
using AviaCompany.Application.Contracts.Flight;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Flights;
using Microsoft.EntityFrameworkCore;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с авиарейсами
/// </summary>
public class FlightService(IRepository<Flight, int> repository, IMapper mapper) : IFlightService
{
    public async Task<FlightDto> Create(FlightCreateUpdateDto dto)
    {
        var flight = mapper.Map<Flight>(dto);
        var result = await repository.Create(flight);
        return mapper.Map<FlightDto>(result);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    public async Task<FlightDto?> Get(int dtoId)
    {
        var flight = await repository.Read(dtoId);
        return flight != null ? mapper.Map<FlightDto>(flight) : null;
    }

    public async Task<IList<FlightDto>> GetAll()
    {
        var flights = await repository.ReadAll();
        return mapper.Map<IList<FlightDto>>(flights);
    }

    public async Task<List<FlightDto>> GetTopFlightsByPassengerCountAsync(int count = 5)
    {
        var flights = await repository.ReadAll();
        var flightsWithPassengerCount = flights
            .Select(f => new
            {
                Flight = f,
                PassengerCount = f.Tickets?.Count ?? 0
            })
            .OrderByDescending(x => x.PassengerCount)
            .Take(count)
            .Select(x => x.Flight)
            .ToList();

        return mapper.Map<List<FlightDto>>(flightsWithPassengerCount);
    }

    public async Task<List<FlightDto>> GetFlightsWithShortestDurationAsync()
    {
        var flights = await repository.ReadAll();
        if (!flights.Any()) return new List<FlightDto>();

        var minDuration = flights.Min(f => f.FlightDuration);
        var shortestFlights = flights
            .Where(f => f.FlightDuration == minDuration)
            .ToList();

        return mapper.Map<List<FlightDto>>(shortestFlights);
    }

    public async Task<List<FlightDto>> GetFlightsByRouteAsync(string departureCity, string arrivalCity)
    {
        var flights = await repository.ReadAll();
        var routeFlights = flights
            .Where(f => f.DepartureCity.Equals(departureCity, StringComparison.OrdinalIgnoreCase) &&
                       f.ArrivalCity.Equals(arrivalCity, StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f.DepartureDate)
            .ThenBy(f => f.DepartureTime)
            .ToList();

        return mapper.Map<List<FlightDto>>(routeFlights);
    }

    public async Task<List<FlightDto>> GetFlightsByModelAndPeriodAsync(int modelId, DateTime from, DateTime to)
    {
        var flights = await repository.ReadAll();
        var filteredFlights = flights
            .Where(f => f.AircraftModelId == modelId &&
                       f.DepartureDate >= from &&
                       f.DepartureDate <= to)
            .ToList();

        return mapper.Map<List<FlightDto>>(filteredFlights);
    }

    public async Task<FlightDto> Update(FlightCreateUpdateDto dto, int dtoId)
    {
        var flight = mapper.Map<Flight>(dto);
        flight.Id = dtoId;
        var result = await repository.Update(flight);
        return mapper.Map<FlightDto>(result);
    }
}