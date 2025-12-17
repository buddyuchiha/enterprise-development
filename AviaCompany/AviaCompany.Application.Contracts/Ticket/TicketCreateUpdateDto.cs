namespace AviaCompany.Application.Contracts.Ticket;

/// <summary>
/// DTO для POST/PUT запросов к билетам
/// </summary>
/// <param name="FlightId">Идентификатор рейса</param>
/// <param name="PassengerId">Идентификатор пассажира</param>
/// <param name="SeatNumber">Номер места</param>
/// <param name="HasHandLuggage">Наличие ручной клади</param>
/// <param name="LuggageWeight">Вес багажа (кг)</param>
public record TicketCreateUpdateDto(
    int FlightId,
    int PassengerId,
    string? SeatNumber,
    bool HasHandLuggage,
    decimal? LuggageWeight
);