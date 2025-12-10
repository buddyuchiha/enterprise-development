using AviaCompany.Domain.Models.Tickets;
using Microsoft.EntityFrameworkCore;

namespace AviaCompany.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для работы с билетами.
/// </summary>
public class TicketEfCoreRepository(AviaCompanyDbContext context)
    : GenericEfCoreRepository<Ticket, int>(context)
{
    /// <summary>
    /// Получает билет по идентификатору, включая связанный рейс и пассажира.
    /// </summary>
    /// <param name="entityId">Идентификатор запрашиваемого билета</param>
    /// <returns>Найденный билет с рейсом и пассажиром или null, если не найден</returns>
    public override async Task<Ticket?> Read(int entityId) =>
        await _dbSet
            .Include(t => t.Flight)
            .Include(t => t.Passenger)
            .FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Получает список всех билетов, включая для каждого связанный рейс и пассажира.
    /// </summary>
    /// <returns>Список всех билетов с рейсами и пассажирами</returns>
    public override async Task<IList<Ticket>> ReadAll() =>
        await _dbSet
            .Include(t => t.Flight)
            .Include(t => t.Passenger)
            .ToListAsync();
}