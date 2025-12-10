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
public class PassengersController(
    IApplicationService<PassengerDto, PassengerCreateUpdateDto, int> crudService,
    ITicketService ticketService,
    ILogger<PassengersController> logger)
    : CrudControllerBase<PassengerDto, PassengerCreateUpdateDto, int>(crudService, logger)
{
    /// <summary>
    /// Получить билеты пассажира
    /// </summary>
    [HttpGet("{passengerId}/tickets")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<TicketDto>>> GetPassengerTickets(int passengerId)
    {
        logger.LogInformation("Получение билетов для пассажира {PassengerId}", passengerId);
        try
        {
            var tickets = await ticketService.GetTicketsByPassengerAsync(passengerId);
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения билетов пассажира");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}