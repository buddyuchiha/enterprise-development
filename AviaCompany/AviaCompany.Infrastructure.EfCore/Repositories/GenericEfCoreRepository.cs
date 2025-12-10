using AviaCompany.Domain;
using Microsoft.EntityFrameworkCore;

namespace AviaCompany.Infrastructure.EfCore.Repositories;

/// <summary>
/// Базовый репозиторий для работы с Entity Framework Core
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public class GenericEfCoreRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    protected readonly AviaCompanyDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Конструктор с контекстом БД
    /// </summary>
    public GenericEfCoreRepository(AviaCompanyDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> Create(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public virtual async Task<bool> Delete(TKey entityId)
    {
        var entity = await _dbSet.FindAsync(entityId);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> Read(TKey entityId)
    {
        return await _dbSet.FindAsync(entityId);
    }

    /// <inheritdoc/>
    public virtual async Task<IList<TEntity>> ReadAll()
    {
        return await _dbSet.ToListAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> Update(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}