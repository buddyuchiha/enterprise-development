using AviaCompany.Application.Contracts.Flight;
using AviaCompany.Application.Contracts.Passenger;
using AviaCompany.Application.Contracts.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace AviaCompany.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с рейсами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FlightController(
    IFlightService flightService,
    ITicketService ticketService,
    IPassengerService passengerService,
    ILogger<FlightController> logger) : CrudControllerBase<FlightDto, FlightCreateUpdateDto, int>(flightService, logger)
{

    /// <summary>
    /// Получить топ N рейсов по количеству пассажиров
    /// </summary>
    /// <param name="count">Количество рейсов (по умолчанию 5)</param>
    /// <returns>Список рейсов</returns>
    [HttpGet("top-by-passengers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<FlightDto>>> GetTopFlightsByPassengerCount(int count = 5)
    {
        logger.LogInformation(
            "{Method} вызван в {Controller} с параметром count={Count}",
            nameof(GetTopFlightsByPassengerCount),
            nameof(FlightController),
            count);

        try
        {
            var result = await flightService.GetTopFlightsByPassengerCountAsync(count);

            logger.LogInformation(
                "{Method} завершился успешно, найдено {Count} рейсов",
                nameof(GetTopFlightsByPassengerCount),
                result.Count);

            return result.Count > 0 ? Ok(result) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(
                "Исключение в {Method}: {Exception}",
                nameof(GetTopFlightsByPassengerCount),
                ex);

            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить рейсы с минимальным временем в пути
    /// </summary>
    /// <returns>Список рейсов</returns>
    [HttpGet("shortest-duration")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<FlightDto>>> GetFlightsWithShortestDuration()
    {
        logger.LogInformation(
            "{Method} вызван в {Controller}",
            nameof(GetFlightsWithShortestDuration),
            nameof(FlightController));

        try
        {
            var result = await flightService.GetFlightsWithShortestDurationAsync();

            logger.LogInformation(
                "{Method} завершился успешно, найдено {Count} рейсов",
                nameof(GetFlightsWithShortestDuration),
                result.Count);

            return result.Count > 0 ? Ok(result) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(
                "Исключение в {Method}: {Exception}",
                nameof(GetFlightsWithShortestDuration),
                ex);

            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить рейсы по маршруту
    /// </summary>
    /// <param name="departureCity">Город отправления</param>
    /// <param name="arrivalCity">Город прибытия</param>
    /// <returns>Список рейсов</returns>
    [HttpGet("by-route")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<FlightDto>>> GetFlightsByRoute(
        [FromQuery] string departureCity,
        [FromQuery] string arrivalCity)
    {
        logger.LogInformation(
            "{Method} вызван в {Controller} с параметрами: departureCity={DepartureCity}, arrivalCity={ArrivalCity}",
            nameof(GetFlightsByRoute),
            nameof(FlightController),
            departureCity,
            arrivalCity);

        try
        {
            var result = await flightService.GetFlightsByRouteAsync(departureCity, arrivalCity);

            logger.LogInformation(
                "{Method} завершился успешно, найдено {Count} рейсов",
                nameof(GetFlightsByRoute),
                result.Count);

            return result.Count > 0 ? Ok(result) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(
                "Исключение в {Method}: {Exception}",
                nameof(GetFlightsByRoute),
                ex);

            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить рейсы модели самолета за указанный период
    /// </summary>
    /// <param name="modelId">ID модели самолета</param>
    /// <param name="from">Начало периода</param>
    /// <param name="to">Конец периода</param>
    /// <returns>Список рейсов</returns>
    [HttpGet("by-model-period/{modelId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<FlightDto>>> GetFlightsByModelAndPeriod(
        int modelId,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        logger.LogInformation(
            "{Method} вызван в {Controller} с параметрами: modelId={ModelId}, from={From}, to={To}",
            nameof(GetFlightsByModelAndPeriod),
            nameof(FlightController),
            modelId,
            from,
            to);

        try
        {
            var result = await flightService.GetFlightsByModelAndPeriodAsync(modelId, from, to);

            logger.LogInformation(
                "{Method} завершился успешно, найдено {Count} рейсов",
                nameof(GetFlightsByModelAndPeriod),
                result.Count);

            return result.Count > 0 ? Ok(result) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(
                "Исключение в {Method}: {Exception}",
                nameof(GetFlightsByModelAndPeriod),
                ex);

            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить билеты рейса
    /// </summary>
    /// <param name="flightId">ID рейса</param>
    /// <returns>Список билетов</returns>
    [HttpGet("{flightId}/tickets")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]    
    public async Task<ActionResult<IList<TicketDto>>> GetFlightTickets(int flightId)
    {
        logger.LogInformation("Получение билетов для рейса {FlightId}", flightId);


        var flightExists = await flightService.Get(flightId) != null;
        if (!flightExists)
        {
            return NotFound($"Рейс с ID {flightId} не найден");
        }

        var tickets = await ticketService.GetTicketsByFlightAsync(flightId);
        return tickets.Count > 0 ? Ok(tickets) : NotFound();
    }

    /// <summary>
    /// Получить билеты пассажиров без багажа
    /// </summary>
    /// <param name="flightId">ID рейса</param>
    /// <returns>Список билетов</returns>
    [HttpGet("{flightId}/passengers-without-baggage")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<PassengerDto>>> GetPassengersWithoutBaggage(int flightId)
    {
        try
        {
            var passengers = await passengerService.GetPassengersWithoutBaggageAsync(flightId);
            return passengers.Count > 0 ? Ok(passengers) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении пассажиров без багажа для рейса {FlightId}", flightId);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}
