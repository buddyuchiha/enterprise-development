using AviaCompany.Domain.Models.Aircrafts;
using Microsoft.EntityFrameworkCore;

namespace AviaCompany.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для работы с семействами самолетов
/// </summary>
public class AircraftFamilyEfCoreRepository : GenericEfCoreRepository<AircraftFamily, int>
{
    public AircraftFamilyEfCoreRepository(AviaCompanyDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Получение семейства с моделями
    /// </summary>
    public override async Task<AircraftFamily?> Read(int entityId)
    {
        return await _dbSet
            .Include(f => f.Models)
            .FirstOrDefaultAsync(e => e.Id == entityId);
    }

    /// <summary>
    /// Получение всех семейств с моделями
    /// </summary>
    public override async Task<IList<AircraftFamily>> ReadAll()
    {
        return await _dbSet
            .Include(f => f.Models)
            .ToListAsync();
    }
}