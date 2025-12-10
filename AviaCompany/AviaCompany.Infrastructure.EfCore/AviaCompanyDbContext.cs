using AviaCompany.Domain.Data;
using AviaCompany.Domain.Models;
using AviaCompany.Domain.Models.Aircrafts;
using AviaCompany.Domain.Models.Flights;
using AviaCompany.Domain.Models.Passengers;
using AviaCompany.Domain.Models.Tickets;
using Microsoft.EntityFrameworkCore;

namespace AviaCompany.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных для авиакомпании
/// </summary>
public class AviaCompanyDbContext : DbContext
{
    /// <summary>
    /// Конструктор с настройками контекста
    /// </summary>
    public AviaCompanyDbContext(DbContextOptions<AviaCompanyDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Семейства самолетов
    /// </summary>
    public DbSet<AircraftFamily> AircraftFamilies { get; set; }

    /// <summary>
    /// Модели самолетов
    /// </summary>
    public DbSet<AircraftModel> AircraftModels { get; set; }

    /// <summary>
    /// Авиарейсы
    /// </summary>
    public DbSet<Flight> Flights { get; set; }

    /// <summary>
    /// Пассажиры
    /// </summary>
    public DbSet<Passenger> Passengers { get; set; }

    /// <summary>
    /// Билеты
    /// </summary>
    public DbSet<Ticket> Tickets { get; set; }

    /// <summary>
    /// Конфигурация модели данных
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Семейство самолетов
        modelBuilder.Entity<AircraftFamily>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(e => e.Models)
                  .WithOne(m => m.Family)
                  .HasForeignKey(m => m.FamilyId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Модель самолета
        modelBuilder.Entity<AircraftModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Family)
                  .WithMany(f => f.Models)
                  .HasForeignKey(e => e.FamilyId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Flights)
                  .WithOne(f => f.AircraftModel)
                  .HasForeignKey(f => f.AircraftModelId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Рейс
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.AircraftModel)
                  .WithMany(m => m.Flights)
                  .HasForeignKey(e => e.AircraftModelId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Tickets)
                  .WithOne(t => t.Flight)
                  .HasForeignKey(t => t.FlightId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Пассажир
        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(e => e.Tickets)
                  .WithOne(t => t.Passenger)
                  .HasForeignKey(t => t.PassengerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Билет
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Flight)
                  .WithMany(f => f.Tickets)
                  .HasForeignKey(e => e.FlightId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Passenger)
                  .WithMany(p => p.Tickets)
                  .HasForeignKey(e => e.PassengerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Заполнение начальными данными
        var seeder = new DataSeeder();

        modelBuilder.Entity<AircraftFamily>().HasData(seeder.AircraftFamilies);
        modelBuilder.Entity<AircraftModel>().HasData(seeder.AircraftModels);
        modelBuilder.Entity<Flight>().HasData(seeder.Flights);
        modelBuilder.Entity<Passenger>().HasData(seeder.Passengers);
        modelBuilder.Entity<Ticket>().HasData(seeder.Tickets);
    }
}