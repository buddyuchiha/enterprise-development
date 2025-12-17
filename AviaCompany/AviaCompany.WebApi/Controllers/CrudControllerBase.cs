using AviaCompany.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AviaCompany.WebApi.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций
/// </summary>
/// <typeparam name="TDto">DTO для GET запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для POST/PUT запросов</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>
    : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Сервис для выполнения CRUD-операций
    /// </summary>
    protected readonly IApplicationService<TDto, TCreateUpdateDto, TKey> AppService;

    /// <summary>
    /// Логгер для записи событий
    /// </summary>
    protected readonly ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> Logger;

    /// <summary>
    /// Конструктор базового контроллера
    /// </summary>
    protected CrudControllerBase(
        IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
        ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger)
    {
        AppService = appService;
        Logger = logger;
    }

    /// <summary>
    /// Создание новой записи
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public virtual async Task<ActionResult<TDto>> Create(TCreateUpdateDto newDto)
    {
        Logger.LogInformation("{method} вызван в {controller}", nameof(Create), GetType().Name);
        try
        {
            var result = await AppService.Create(newDto);
            Logger.LogInformation("{method} завершился успешно", nameof(Create));
            return CreatedAtAction(nameof(Create), result);
        }
        catch (Exception ex)
        {
            Logger.LogError("Исключение в {method}: {Exception}", nameof(Create), ex);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Редактирование записи
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public virtual async Task<ActionResult<TDto>> Edit(TKey id, TCreateUpdateDto newDto)
    {
        Logger.LogInformation("{method} вызван в {controller}", nameof(Edit), GetType().Name);
        try
        {
            var result = await AppService.Update(newDto, id);
            Logger.LogInformation("{method} завершился успешно", nameof(Edit));
            return Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError("Исключение в {method}: {Exception}", nameof(Edit), ex);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Удаление записи
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public virtual async Task<IActionResult> Delete(TKey id)
    {
        Logger.LogInformation("{method} вызван в {controller}", nameof(Delete), GetType().Name);
        try
        {
            var result = await AppService.Delete(id);
            Logger.LogInformation("{method} завершился успешно", nameof(Delete));
            return result ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError("Исключение в {method}: {Exception}", nameof(Delete), ex);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получение всех записей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public virtual async Task<ActionResult<IList<TDto>>> GetAll()
    {
        Logger.LogInformation("{method} вызван в {controller}", nameof(GetAll), GetType().Name);
        try
        {
            var result = await AppService.GetAll();
            Logger.LogInformation("{method} завершился успешно", nameof(GetAll));
            return Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError("Исключение в {method}: {Exception}", nameof(GetAll), ex);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получение записи по ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public virtual async Task<ActionResult<TDto>> Get(TKey id)
    {
        Logger.LogInformation("{method} вызван в {controller}", nameof(Get), GetType().Name);
        try
        {
            var result = await AppService.Get(id);
            Logger.LogInformation("{method} завершился успешно", nameof(Get));
            return result != null ? Ok(result) : NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError("Исключение в {method}: {Exception}", nameof(Get), ex);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }
}