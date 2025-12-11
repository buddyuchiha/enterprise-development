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
    /// <summary>
    /// Создает новое семейство самолетов
    /// </summary>
    /// <param name="dto">DTO для создания или обновления семейства самолетов</param>
    /// <returns>DTO созданного семейства самолетов</returns>
    public async Task<AircraftFamilyDto> Create(AircraftFamilyCreateUpdateDto dto)
    {
        var family = mapper.Map<AircraftFamily>(dto);
        var result = await repository.Create(family);
        return mapper.Map<AircraftFamilyDto>(result);
    }

    /// <summary>
    /// Удаляет семейство самолетов по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор семейства самолетов</param>
    /// <returns>Результат удаления (true - успешно, false - неудачно)</returns>
    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Получает семейство самолетов по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор семейства самолетов</param>
    /// <returns>DTO семейства самолетов или null, если не найдено</returns>
    public async Task<AircraftFamilyDto?> Get(int dtoId)
    {
        var family = await repository.Read(dtoId);
        return family != null ? mapper.Map<AircraftFamilyDto>(family) : null;
    }

    /// <summary>
    /// Получает все семейства самолетов
    /// </summary>
    /// <returns>Список DTO всех семейств самолетов</returns>
    public async Task<IList<AircraftFamilyDto>> GetAll()
    {
        var families = await repository.ReadAll();
        return mapper.Map<IList<AircraftFamilyDto>>(families);
    }

    /// <summary>
    /// Обновляет семейство самолетов
    /// </summary>
    /// <param name="dto">DTO для создания или обновления семейства самолетов</param>
    /// <param name="dtoId">Идентификатор обновляемого семейства самолетов</param>
    /// <returns>DTO обновленного семейства самолетов</returns>
    public async Task<AircraftFamilyDto> Update(AircraftFamilyCreateUpdateDto dto, int dtoId)
    {
        var family = mapper.Map<AircraftFamily>(dto);
        family.Id = dtoId;
        var result = await repository.Update(family);
        return mapper.Map<AircraftFamilyDto>(result);
    }
}