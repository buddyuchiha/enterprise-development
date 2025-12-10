namespace AviaCompany.Application.Contracts.Flight;

/// <summary>
/// DTO для POST/PUT запросов к авиарейсам
/// </summary>
/// <param name="Code">Код рейса</param>
/// <param name="DepartureCity">Город отправления</param>
/// <param name="ArrivalCity">Город прибытия</param>
/// <param name="DepartureDate">Дата и время отправления</param>
/// <param name="ArrivalDate">Дата и время прибытия</param>
/// <param name="DepartureTime">Время отправления</param>
/// <param name="FlightDuration">Время в пути</param>
/// <param name="AircraftModelId">Идентификатор модели самолета</param>
public record FlightCreateUpdateDto(
    string? Code,
    string? DepartureCity,
    string? ArrivalCity,
    DateTime? DepartureDate,
    DateTime? ArrivalDate,
    TimeSpan? DepartureTime,
    TimeSpan? FlightDuration,
    int AircraftModelId
);