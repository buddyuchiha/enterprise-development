using AutoMapper;
using AviaCompany.Application.Contracts.AircraftFamily;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Aircrafts;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с семействами самолетов
/// </summary>
public class AircraftFamilyService : IAircraftFamilyService
{
    private readonly IRepository<AircraftFamily, int> _repository;
    private readonly IMapper _mapper;

    public AircraftFamilyService(IRepository<AircraftFamily, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AircraftFamilyDto> Create(AircraftFamilyCreateUpdateDto dto)
    {
        var family = _mapper.Map<AircraftFamily>(dto);
        var result = await _repository.Create(family);
        return _mapper.Map<AircraftFamilyDto>(result);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await _repository.Delete(dtoId);
    }

    public async Task<AircraftFamilyDto?> Get(int dtoId)
    {
        var family = await _repository.Read(dtoId);
        return family != null ? _mapper.Map<AircraftFamilyDto>(family) : null;
    }

    public async Task<IList<AircraftFamilyDto>> GetAll()
    {
        var families = await _repository.ReadAll();
        return _mapper.Map<IList<AircraftFamilyDto>>(families);
    }

    public async Task<AircraftFamilyDto> Update(AircraftFamilyCreateUpdateDto dto, int dtoId)
    {
        var family = _mapper.Map<AircraftFamily>(dto);
        family.Id = dtoId;
        var result = await _repository.Update(family);
        return _mapper.Map<AircraftFamilyDto>(result);
    }
}