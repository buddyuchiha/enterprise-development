using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaCompany.Application.Contracts;

/// <summary>
/// Интерфейс службы приложения для CRUD операций
/// </summary>
/// <typeparam name="TDto">DTO для Get-запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для Post/Put-запросов</typeparam>
/// <typeparam name="TKey">Тип идентификатора DTO</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создание DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>Созданная DTO</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получение DTO по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор DTO</param>
    /// <returns>Найденная DTO или null</returns>
    public Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Получение всего списка DTO
    /// </summary>
    /// <returns>Список всех DTO</returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Обновление DTO
    /// </summary>
    /// <param name="dto">DTO с новыми данными</param>
    /// <param name="dtoId">Идентификатор DTO</param> 
    /// <returns>Обновленная DTO</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Удаление DTO
    /// </summary>
    /// <param name="dtoId">Идентификатор DTO</param>
    /// <returns>True если удалено, false если не найдено</returns>
    public Task<bool> Delete(TKey dtoId);
}
