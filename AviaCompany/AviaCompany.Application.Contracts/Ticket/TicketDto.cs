namespace AviaCompany.Application.Contracts.Ticket;

/// <summary>
/// DTO для GET запросов к билетам
/// </summary>
/// <param name="Id">Идентификатор билета</param>
/// <param name="FlightId">Идентификатор рейса</param>
/// <param name="PassengerId">Идентификатор пассажира</param>
/// <param name="SeatNumber">Номер места</param>
/// <param name="HasHandLuggage">Наличие ручной клади</param>
/// <param name="LuggageWeight">Вес багажа (кг)</param>
public record TicketDto(
    int Id,
    int FlightId,
    int PassengerId,
    string? SeatNumber,
    bool? HasHandLuggage,
    decimal? LuggageWeight
);