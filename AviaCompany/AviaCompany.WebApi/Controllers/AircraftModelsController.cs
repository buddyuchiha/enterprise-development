using AviaCompany.Application.Contracts;
using AviaCompany.Application.Contracts.AircraftModel;
using AviaCompany.Application.Contracts.Flight;
using Microsoft.AspNetCore.Mvc;

namespace AviaCompany.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с моделями самолетов
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AircraftModelsController(
    IApplicationService<AircraftModelDto, AircraftModelCreateUpdateDto, int> crudService,
    IFlightService flightService,
    ILogger<AircraftModelsController> logger)
    : CrudControllerBase<AircraftModelDto, AircraftModelCreateUpdateDto, int>(crudService, logger)
{
    /// <summary>
    /// Получить рейсы модели в указанный период
    /// </summary>
    [HttpGet("{modelId}/flights/period")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<FlightDto>>> GetFlightsByModelAndPeriod(
        int modelId,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        logger.LogInformation("Получение рейсов модели {ModelId} с {From} по {To}", modelId, from, to);
        try
        {
            var flights = await flightService.GetFlightsByModelAndPeriodAsync(modelId, from, to);
            return Ok(flights);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения рейсов по модели и периоду");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}