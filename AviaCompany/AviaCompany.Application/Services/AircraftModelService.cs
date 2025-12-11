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
    /// <summary>
    /// Создает новую модель самолета
    /// </summary>
    /// <param name="dto">DTO для создания или обновления модели самолета</param>
    /// <returns>DTO созданной модели самолета</returns>
    public async Task<AircraftModelDto> Create(AircraftModelCreateUpdateDto dto)
    {
        var model = mapper.Map<AircraftModel>(dto);
        var result = await repository.Create(model);
        return mapper.Map<AircraftModelDto>(result);
    }

    /// <summary>
    /// Удаляет модель самолета по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор модели самолета</param>
    /// <returns>Результат удаления (true - успешно, false - неудачно)</returns>
    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Получает модель самолета по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор модели самолета</param>
    /// <returns>DTO модели самолета или null, если не найдено</returns>
    public async Task<AircraftModelDto?> Get(int dtoId)
    {
        var model = await repository.Read(dtoId);
        return model != null ? mapper.Map<AircraftModelDto>(model) : null;
    }

    /// <summary>
    /// Получает все модели самолетов
    /// </summary>
    /// <returns>Список DTO всех моделей самолетов</returns>
    public async Task<IList<AircraftModelDto>> GetAll()
    {
        var models = await repository.ReadAll();
        return mapper.Map<IList<AircraftModelDto>>(models);
    }

    /// <summary>
    /// Получает список рейсов для указанной модели самолета
    /// </summary>
    /// <param name="modelId">Идентификатор модели самолета</param>
    /// <returns>Список DTO рейсов, связанных с моделью самолета</returns>
    public async Task<IList<FlightDto>> GetFlightsByModel(int modelId)
    {
        var allFlights = await flightRepository.ReadAll();
        var flights = allFlights
            .Where(f => f.AircraftModelId == modelId)
            .ToList();

        return mapper.Map<IList<FlightDto>>(flights);
    }

    /// <summary>
    /// Обновляет модель самолета
    /// </summary>
    /// <param name="dto">DTO для создания или обновления модели самолета</param>
    /// <param name="dtoId">Идентификатор обновляемой модели самолета</param>
    /// <returns>DTO обновленной модели самолета</returns>
    public async Task<AircraftModelDto> Update(AircraftModelCreateUpdateDto dto, int dtoId)
    {
        var model = mapper.Map<AircraftModel>(dto);
        model.Id = dtoId;
        var result = await repository.Update(model);
        return mapper.Map<AircraftModelDto>(result);
    }
}