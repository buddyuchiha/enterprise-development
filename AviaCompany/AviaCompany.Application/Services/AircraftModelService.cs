using AutoMapper;
using AviaCompany.Application.Contracts.AircraftModel;
using AviaCompany.Application.Contracts.Flight;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Aircrafts;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с моделями самолетов
/// </summary>
public class AircraftModelService : IAircraftModelService
{
    private readonly IRepository<AircraftModel, int> _repository;
    private readonly IMapper _mapper;

    public AircraftModelService(IRepository<AircraftModel, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AircraftModelDto> Create(AircraftModelCreateUpdateDto dto)
    {
        var model = _mapper.Map<AircraftModel>(dto);
        var result = await _repository.Create(model);
        return _mapper.Map<AircraftModelDto>(result);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await _repository.Delete(dtoId);
    }

    public async Task<AircraftModelDto?> Get(int dtoId)
    {
        var model = await _repository.Read(dtoId);
        return model != null ? _mapper.Map<AircraftModelDto>(model) : null;
    }

    public async Task<IList<AircraftModelDto>> GetAll()
    {
        var models = await _repository.ReadAll();
        return _mapper.Map<IList<AircraftModelDto>>(models);
    }

    public async Task<IList<FlightDto>> GetFlightsByModelAndPeriodAsync(int modelId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public async Task<AircraftModelDto> Update(AircraftModelCreateUpdateDto dto, int dtoId)
    {
        var model = _mapper.Map<AircraftModel>(dto);
        model.Id = dtoId;
        var result = await _repository.Update(model);
        return _mapper.Map<AircraftModelDto>(result);
    }
}