using AviaCompany.Application.Contracts;
using AviaCompany.Application.Contracts.AircraftFamily;
using Microsoft.AspNetCore.Mvc;

namespace AviaCompany.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с семействами самолетов
/// </summary>
public class AircraftFamiliesController : CrudControllerBase<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, int>
{
    private readonly IAircraftFamilyService _aircraftFamilyService;

    /// <summary>
    /// Конструктор контроллера семейств самолетов
    /// </summary>
    /// <param name="aircraftFamilyService">Сервис для работы с семействами самолетов</param>
    /// <param name="logger">Логгер для записи событий</param>
    public AircraftFamiliesController(
        IAircraftFamilyService aircraftFamilyService,  
        ILogger<AircraftFamiliesController> logger)
        : base(aircraftFamilyService, logger)
    {
        _aircraftFamilyService = aircraftFamilyService;
    }
}