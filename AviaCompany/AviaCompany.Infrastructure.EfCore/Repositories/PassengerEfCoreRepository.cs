using AviaCompany.Domain.Models.Passengers;
using Microsoft.EntityFrameworkCore;

namespace AviaCompany.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для работы с пассажирами
/// </summary>
public class PassengerEfCoreRepository : GenericEfCoreRepository<Passenger, int>
{
    public PassengerEfCoreRepository(AviaCompanyDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Получение пассажира с билетами
    /// </summary>
    public override async Task<Passenger?> Read(int entityId)
    {
        return await _dbSet
            .Include(p => p.Tickets)
            .ThenInclude(t => t.Flight)
            .FirstOrDefaultAsync(e => e.Id == entityId);
    }

    /// <summary>
    /// Получение всех пассажиров с билетами
    /// </summary>
    public override async Task<IList<Passenger>> ReadAll()
    {
        return await _dbSet
            .Include(p => p.Tickets)
            .ThenInclude(t => t.Flight)
            .ToListAsync();
    }
}