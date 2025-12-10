using AviaCompany.Application.Contracts;
using AviaCompany.Application.Contracts.Flight;

namespace AviaCompany.Application.Contracts.AircraftModel;

/// <summary>
/// Сервис для работы с моделями самолетов
/// </summary>
public interface IAircraftModelService : IApplicationService<AircraftModelDto, AircraftModelCreateUpdateDto, int>
{
    /// <summary>
    /// Получает рейсы, выполняемые на указанной модели самолёта в заданный период
    /// </summary>
    /// <param name="modelId">Идентификатор модели</param>
    /// <param name="from">Начало периода</param>
    /// <param name="to">Конец периода</param>
    /// <returns>Список рейсов по модели и периоду</returns>
    public Task<IList<FlightDto>> GetFlightsByModelAndPeriodAsync(int modelId, DateTime from, DateTime to);
}