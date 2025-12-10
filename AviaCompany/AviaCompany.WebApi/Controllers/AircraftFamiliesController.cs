using AviaCompany.Application.Contracts;
using AviaCompany.Application.Contracts.AircraftFamily;
using Microsoft.AspNetCore.Mvc;

namespace AviaCompany.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с семействами самолетов
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AircraftFamiliesController(
    IApplicationService<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, int> crudService,
    ILogger<AircraftFamiliesController> logger)
    : CrudControllerBase<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, int>(crudService, logger);