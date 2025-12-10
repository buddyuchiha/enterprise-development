namespace AviaCompany.Domain;

/// <summary>
/// Интерфейс репозитория для CRUD операций
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Создание новой сущности
    /// </summary>
    /// <param name="entity">Новая сущность</param>
    /// <returns>Созданная сущность</returns>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Получение сущности по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>Найденная сущность или null</returns>
    public Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Получение всего списка сущностей
    /// </summary> 
    /// <returns>Список всех сущностей</returns>
    public Task<IList<TEntity>> ReadAll();

    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="entity">Отредактированная сущность</param>
    /// <returns>Обновленная сущность</returns>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>True если удалено, false если не найдено</returns>
    public Task<bool> Delete(TKey entityId);
}