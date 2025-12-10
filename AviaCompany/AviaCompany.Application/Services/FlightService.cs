using AutoMapper;
using AviaCompany.Application.Contracts.Flight;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Flights;
using Microsoft.EntityFrameworkCore;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с авиарейсами
/// </summary>
public class FlightService : IFlightService
{
    private readonly IRepository<Flight, int> _repository;
    private readonly IMapper _mapper;

    public FlightService(IRepository<Flight, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FlightDto> Create(FlightCreateUpdateDto dto)
    {
        var flight = _mapper.Map<Flight>(dto);
        var result = await _repository.Create(flight);
        return _mapper.Map<FlightDto>(result);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await _repository.Delete(dtoId);
    }

    public async Task<FlightDto?> Get(int dtoId)
    {
        var flight = await _repository.Read(dtoId);
        return flight != null ? _mapper.Map<FlightDto>(flight) : null;
    }

    public async Task<IList<FlightDto>> GetAll()
    {
        var flights = await _repository.ReadAll();
        return _mapper.Map<IList<FlightDto>>(flights);
    }

    public async Task<List<FlightDto>> GetTopFlightsByPassengerCountAsync(int count = 5)
    {
        throw new NotImplementedException();
    }

    public async Task<List<FlightDto>> GetFlightsWithShortestDurationAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<FlightDto>> GetFlightsByRouteAsync(string departureCity, string arrivalCity)
    {
        throw new NotImplementedException();
    }

    public async Task<FlightDto> Update(FlightCreateUpdateDto dto, int dtoId)
    {
        var flight = _mapper.Map<Flight>(dto);
        flight.Id = dtoId;
        var result = await _repository.Update(flight);
        return _mapper.Map<FlightDto>(result);
    }
}