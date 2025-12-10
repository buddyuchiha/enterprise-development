namespace AviaCompany.Application.Contracts.Flight;

/// <summary>
/// DTO для GET запросов к авиарейсам
/// </summary>
/// <param name="Id">Идентификатор рейса</param>
/// <param name="Code">Код рейса (например, "SU1001")</param>
/// <param name="DepartureCity">Город отправления</param>
/// <param name="ArrivalCity">Город прибытия</param>
/// <param name="DepartureDate">Дата и время отправления</param>
/// <param name="ArrivalDate">Дата и время прибытия</param>
/// <param name="DepartureTime">Время отправления</param>
/// <param name="FlightDuration">Время в пути</param>
/// <param name="AircraftModelId">Идентификатор модели самолета</param>
public record FlightDto(
    int Id,
    string? Code,
    string? DepartureCity,
    string? ArrivalCity,
    DateTime? DepartureDate,
    DateTime? ArrivalDate,
    TimeSpan? DepartureTime,
    TimeSpan? FlightDuration,
    int AircraftModelId
);