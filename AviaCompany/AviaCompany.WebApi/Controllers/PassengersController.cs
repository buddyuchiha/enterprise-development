using AviaCompany.Application.Contracts;
using AviaCompany.Application.Contracts.Passenger;
using AviaCompany.Application.Contracts.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace AviaCompany.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с пассажирами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PassengersController : CrudControllerBase<PassengerDto, PassengerCreateUpdateDto, int>
{
    private readonly IPassengerService _passengerService;
    private readonly ITicketService _ticketService;
    private readonly ILogger<PassengersController> _logger;

    /// <summary>
    /// Конструктор контроллера пассажиров
    /// </summary>
    /// <param name="passengerService">Сервис для работы с пассажирами</param>
    /// <param name="ticketService">Сервис для работы с билетами</param>
    /// <param name="logger">Логгер для записи событий</param>
    public PassengersController(
        IPassengerService passengerService,  
        ITicketService ticketService,
        ILogger<PassengersController> logger)
        : base(passengerService, logger)  
    {
        _passengerService = passengerService;
        _ticketService = ticketService;
        _logger = logger;
    }
    /// <summary>
    /// Получить билеты пассажира
    /// </summary>
    [HttpGet("{passengerId}/tickets")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<TicketDto>>> GetPassengerTickets(int passengerId)
    {
        _logger.LogInformation("Получение билетов для пассажира {PassengerId}", passengerId);
        try
        {
            var tickets = await _ticketService.GetTicketsByPassengerAsync(passengerId);
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка получения билетов пассажира");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}