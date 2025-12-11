using AviaCompany.Application.Contracts.Ticket;

namespace AviaCompany.Application.Contracts.Passenger;

/// <summary>
/// Сервис для работы с пассажирами
/// </summary>
public interface IPassengerService : IApplicationService<PassengerDto, PassengerCreateUpdateDto, int>
{
    /// <summary>
    /// Получает все билеты для указанного пассажира
    /// </summary>
    /// <param name="passengerId">Идентификатор пассажира</param>
    /// <returns>Список билетов пассажира</returns>
    public Task<IList<TicketDto>> GetPassengerTicketsAsync(int passengerId);

    /// <summary>
    /// Получить пассажиров рейса без багажа
    /// </summary>
    /// <param name="flightId">ID рейса</param>
    /// <returns>Список пассажиров без багажа на указанном рейсе</returns>
    public Task<IList<PassengerDto>> GetPassengersWithoutBaggageAsync(int flightId);
}