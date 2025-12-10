using AviaCompany.Application.Contracts;
using AviaCompany.Application.Contracts.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace AviaCompany.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с билетами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TicketsController(
    IApplicationService<TicketDto, TicketCreateUpdateDto, int> crudService,
    ILogger<TicketsController> logger)
    : CrudControllerBase<TicketDto, TicketCreateUpdateDto, int>(crudService, logger);