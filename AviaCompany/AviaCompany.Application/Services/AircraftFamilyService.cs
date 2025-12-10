using AutoMapper;
using AviaCompany.Application.Contracts.AircraftFamily;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Aircrafts;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с семействами самолетов
/// </summary>
public class AircraftFamilyService(IRepository<AircraftFamily, int> repository, IMapper mapper) : IAircraftFamilyService
{
    public async Task<AircraftFamilyDto> Create(AircraftFamilyCreateUpdateDto dto)
    {
        var family = mapper.Map<AircraftFamily>(dto);
        var result = await repository.Create(family);
        return mapper.Map<AircraftFamilyDto>(result);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    public async Task<AircraftFamilyDto?> Get(int dtoId)
    {
        var family = await repository.Read(dtoId);
        return family != null ? mapper.Map<AircraftFamilyDto>(family) : null;
    }

    public async Task<IList<AircraftFamilyDto>> GetAll()
    {
        var families = await repository.ReadAll();
        return mapper.Map<IList<AircraftFamilyDto>>(families);
    }

    public async Task<AircraftFamilyDto> Update(AircraftFamilyCreateUpdateDto dto, int dtoId)
    {
        var family = mapper.Map<AircraftFamily>(dto);
        family.Id = dtoId;
        var result = await repository.Update(family);
        return mapper.Map<AircraftFamilyDto>(result);
    }
}