using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AviaCompany.Application.Contracts.Ticket;

namespace AviaCompany.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с билетами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TicketsController : CrudControllerBase<TicketDto, TicketCreateUpdateDto, int>
{
    private readonly ITicketService _ticketService;
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(
        ITicketService ticketService,  // Изменяем на конкретный сервис
        ILogger<TicketsController> logger)
        : base(ticketService, logger)  // Передаем в базовый
    {
        _ticketService = ticketService;
        _logger = logger;
    }

    /// <summary>
    /// Получить билеты по пассажиру
    /// </summary>
    /// <param name="passengerId">ID пассажира</param>
    /// <returns>Список билетов пассажира</returns>
    [HttpGet("by-passenger/{passengerId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TicketDto>>> GetByPassenger(int passengerId)
    {
        try
        {
            var tickets = await _ticketService.GetTicketsByPassengerAsync(passengerId);
            return tickets.Count > 0 ? Ok(tickets) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении билетов пассажира {PassengerId}", passengerId);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить билеты по рейсу
    /// </summary>
    /// <param name="flightId">ID рейса</param>
    /// <returns>Список билетов рейса</returns>
    [HttpGet("by-flight/{flightId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TicketDto>>> GetByFlight(int flightId)
    {
        try
        {
            var tickets = await _ticketService.GetTicketsByFlightAsync(flightId);
            return tickets.Count > 0 ? Ok(tickets) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении билетов рейса {FlightId}", flightId);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}