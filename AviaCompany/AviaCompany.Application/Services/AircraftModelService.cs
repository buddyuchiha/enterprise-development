using AutoMapper;
using AviaCompany.Application.Contracts.AircraftModel;
using AviaCompany.Application.Contracts.Flight;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Aircrafts;
using AviaCompany.Domain.Models.Flights;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с моделями самолетов
/// </summary>
public class AircraftModelService(IRepository<AircraftModel, int> repository, IRepository<Flight, int> flightRepository, IMapper mapper) : IAircraftModelService
{
    public async Task<AircraftModelDto> Create(AircraftModelCreateUpdateDto dto)
    {
        var model = mapper.Map<AircraftModel>(dto);
        var result = await repository.Create(model);
        return mapper.Map<AircraftModelDto>(result);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    public async Task<AircraftModelDto?> Get(int dtoId)
    {
        var model = await repository.Read(dtoId);
        return model != null ? mapper.Map<AircraftModelDto>(model) : null;
    }

    public async Task<IList<AircraftModelDto>> GetAll()
    {
        var models = await repository.ReadAll();
        return mapper.Map<IList<AircraftModelDto>>(models);
    }

    public async Task<IList<FlightDto>> GetFlightsByModelAndPeriodAsync(int modelId, DateTime from, DateTime to)
    {
        var allFlights = await flightRepository.ReadAll();
        var flights = allFlights
            .Where(f => f.AircraftModelId == modelId &&
                       f.DepartureDate >= from &&
                       f.DepartureDate <= to)
            .ToList();

        return mapper.Map<IList<FlightDto>>(flights);
    }

    public async Task<AircraftModelDto> Update(AircraftModelCreateUpdateDto dto, int dtoId)
    {
        var model = mapper.Map<AircraftModel>(dto);
        model.Id = dtoId;
        var result = await repository.Update(model);
        return mapper.Map<AircraftModelDto>(result);
    }
}