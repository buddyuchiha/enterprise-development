using AutoMapper;
using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Tickets;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с билетами
/// </summary>
public class TicketService(IRepository<Ticket, int> repository, IMapper mapper) : ITicketService
{

    /// <summary>
    /// Создает новый билет
    /// </summary>
    /// <param name="dto">DTO для создания или обновления билета</param>
    /// <returns>DTO созданного билета</returns>
    public async Task<TicketDto> Create(TicketCreateUpdateDto dto)
    {
        var ticket = mapper.Map<Ticket>(dto);
        var result = await repository.Create(ticket);
        return mapper.Map<TicketDto>(result);
    }

    /// <summary>
    /// Удаляет билет по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор билета</param>
    /// <returns>Результат удаления (true - успешно, false - неудачно)</returns>
    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Получает билет по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор билета</param>
    /// <returns>DTO билета или null, если не найдено</returns>
    public async Task<TicketDto?> Get(int dtoId)
    {
        var ticket = await repository.Read(dtoId);
        return ticket != null ? mapper.Map<TicketDto>(ticket) : null;
    }

    /// <summary>
    /// Получает все билеты
    /// </summary>
    /// <returns>Список DTO всех билетов</returns>
    public async Task<IList<TicketDto>> GetAll()
    {
        var tickets = await repository.ReadAll();
        return mapper.Map<IList<TicketDto>>(tickets);
    }

    /// <summary>
    /// Получает билеты для указанного рейса
    /// </summary>
    /// <param name="flightId">Идентификатор рейса</param>
    /// <returns>Список DTO билетов для указанного рейса</returns>
    public async Task<IList<TicketDto>> GetTicketsByFlightAsync(int flightId)
    {
        var allTickets = await repository.ReadAll();
        var flightTickets = allTickets
            .Where(t => t.FlightId == flightId)
            .OrderBy(t => t.SeatNumber)
            .ToList();

        return mapper.Map<IList<TicketDto>>(flightTickets);
    }

    /// <summary>
    /// Получает билеты для указанного пассажира
    /// </summary>
    /// <param name="passengerId">Идентификатор пассажира</param>
    /// <returns>Список DTO билетов для указанного пассажира</returns>
    public async Task<IList<TicketDto>> GetTicketsByPassengerAsync(int passengerId)
    {
        var allTickets = await repository.ReadAll();
        var passengerTickets = allTickets
            .Where(t => t.PassengerId == passengerId)
            .OrderBy(t => t.Id)
            .ToList();

        return mapper.Map<IList<TicketDto>>(passengerTickets);
    }

    /// <summary>
    /// Обновляет билет
    /// </summary>
    /// <param name="dto">DTO для создания или обновления билета</param>
    /// <param name="dtoId">Идентификатор обновляемого билета</param>
    /// <returns>DTO обновленного билета</returns>
    public async Task<TicketDto> Update(TicketCreateUpdateDto dto, int dtoId)
    {
        var ticket = mapper.Map<Ticket>(dto);
        ticket.Id = dtoId;
        var result = await repository.Update(ticket);
        return mapper.Map<TicketDto>(result);
    }
}