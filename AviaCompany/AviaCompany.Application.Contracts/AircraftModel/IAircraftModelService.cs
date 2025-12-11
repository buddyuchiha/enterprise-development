using AviaCompany.Application.Contracts.Flight;

namespace AviaCompany.Application.Contracts.AircraftModel;

/// <summary>
/// Сервис для работы с моделями самолетов
/// </summary>
public interface IAircraftModelService : IApplicationService<AircraftModelDto, AircraftModelCreateUpdateDto, int>
{
    /// <summary>
    /// Получает рейсы, выполняемые на указанной модели самолёта 
    /// </summary>
    /// <param name="modelId">Идентификатор модели</param>
    /// <returns>Список рейсов по модели и периоду</returns>
    public Task<IList<FlightDto>> GetFlightsByModel(int modelId);
}