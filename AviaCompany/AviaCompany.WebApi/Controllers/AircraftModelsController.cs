using AviaCompany.Application.Contracts.AircraftModel;
using AviaCompany.Application.Contracts.Flight;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AviaCompany.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с моделями самолетов
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AircraftModelsController : CrudControllerBase<AircraftModelDto, AircraftModelCreateUpdateDto, int>
{
    private readonly IAircraftModelService _aircraftModelService;
    private readonly ILogger<AircraftModelsController> _logger;

    public AircraftModelsController(
        IAircraftModelService aircraftModelService, 
        ILogger<AircraftModelsController> logger)
        : base(aircraftModelService, logger)
    {
        _aircraftModelService = aircraftModelService;
        _logger = logger;
    }

    /// <summary>
    /// Получить все рейсы модели
    /// </summary>
    /// <param name="id">ID модели</param>
    /// <returns>Список рейсов</returns>
    [HttpGet("{id}/flights")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<FlightDto>>> GetFlights(int id)
    {
        try
        {
            var model = await _aircraftModelService.Get(id);

            var flights = await _aircraftModelService.GetFlightsByModel(id);

            return flights.Count > 0 ? Ok(flights) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении рейсов модели {ModelId}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}