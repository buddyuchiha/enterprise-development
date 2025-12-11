using AutoMapper;
using AviaCompany.Application.Contracts.Passenger;
using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Passengers;
using AviaCompany.Domain.Models.Tickets;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с пассажирами
/// </summary>
public class PassengerService(
    IRepository<Passenger, int> repository,
    IRepository<Ticket, int> ticketRepository,
    IRepository<Passenger, int> passengerRepository,
    IMapper mapper) : IPassengerService
{
    /// <summary>
    /// Создает нового пассажира
    /// </summary>
    /// <param name="dto">DTO для создания или обновления пассажира</param>
    /// <returns>DTO созданного пассажира</returns>
    public async Task<PassengerDto> Create(PassengerCreateUpdateDto dto)
    {
        var passenger = mapper.Map<Passenger>(dto);
        var result = await repository.Create(passenger);
        return mapper.Map<PassengerDto>(result);
    }

    /// <summary>
    /// Удаляет пассажира по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор пассажира</param>
    /// <returns>Результат удаления (true - успешно, false - неудачно)</returns>
    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Получает пассажира по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор пассажира</param>
    /// <returns>DTO пассажира или null, если не найдено</returns>
    public async Task<PassengerDto?> Get(int dtoId)
    {
        var passenger = await repository.Read(dtoId);
        return passenger != null ? mapper.Map<PassengerDto>(passenger) : null;
    }

    /// <summary>
    /// Получает всех пассажиров
    /// </summary>
    /// <returns>Список DTO всех пассажиров</returns>
    public async Task<IList<PassengerDto>> GetAll()
    {
        var passengers = await repository.ReadAll();
        return mapper.Map<IList<PassengerDto>>(passengers);
    }

    /// <summary>
    /// Получает билеты указанного пассажира
    /// </summary>
    /// <param name="passengerId">Идентификатор пассажира</param>
    /// <returns>Список DTO билетов пассажира</returns>
    public async Task<IList<TicketDto>> GetPassengerTicketsAsync(int passengerId)
    {
        var allTickets = await ticketRepository.ReadAll();

        var passengerTickets = allTickets
            .Where(ticket => ticket.PassengerId == passengerId)
            .OrderBy(ticket => ticket.FlightId)
            .ThenBy(ticket => ticket.SeatNumber)
            .ToList();

        return mapper.Map<IList<TicketDto>>(passengerTickets);
    }

    /// <summary>
    /// Получает пассажиров без багажа на указанном рейсе
    /// </summary>
    /// <param name="flightId">Идентификатор рейса</param>
    /// <returns>Список DTO пассажиров без багажа на рейсе</returns>
    public async Task<IList<PassengerDto>> GetPassengersWithoutBaggageAsync(int flightId)
    {
        var allTickets = await ticketRepository.ReadAll();
        var allPassengers = await passengerRepository.ReadAll();

        var passengerIdsWithoutBaggage = allTickets
            .Where(t => t.FlightId == flightId &&
                       (t.LuggageWeight == 0 || t.HasHandLuggage == false))
            .Select(t => t.PassengerId)
            .Distinct()
            .ToList();

        var passengers = allPassengers
            .Where(p => passengerIdsWithoutBaggage.Contains(p.Id))
            .OrderBy(p => p.FullName)
            .ToList();

        return mapper.Map<IList<PassengerDto>>(passengers);
    }

    /// <summary>
    /// Обновляет данные пассажира
    /// </summary>
    /// <param name="dto">DTO для создания или обновления пассажира</param>
    /// <param name="dtoId">Идентификатор обновляемого пассажира</param>
    /// <returns>DTO обновленного пассажира</returns>
    public async Task<PassengerDto> Update(PassengerCreateUpdateDto dto, int dtoId)
    {
        var passenger = mapper.Map<Passenger>(dto);
        passenger.Id = dtoId;
        var result = await repository.Update(passenger);
        return mapper.Map<PassengerDto>(result);
    }
}