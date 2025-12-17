using System.Text;
using AviaCompany.Domain.Data;
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
    public DbSet<AircraftFamily> AircraftFamilies { get; set; } = null!;

    /// <summary>
    /// Модели самолетов
    /// </summary>
    public DbSet<AircraftModel> AircraftModels { get; set; } = null!;

    /// <summary>
    /// Авиарейсы
    /// </summary>
    public DbSet<Flight> Flights { get; set; } = null!;

    /// <summary>
    /// Пассажиры
    /// </summary>
    public DbSet<Passenger> Passengers { get; set; } = null!;

    /// <summary>
    /// Билеты
    /// </summary>
    public DbSet<Ticket> Tickets { get; set; } = null!;

    /// <summary>
    /// Конфигурация модели данных
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
            {
                entity.SetTableName(ToSnakeCase(tableName));
            }

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }

            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var entityTableName = entity.GetTableName() ?? string.Empty;
                var principalTableName = foreignKey.PrincipalEntityType.GetTableName() ?? string.Empty;
                var columnName = foreignKey.Properties.FirstOrDefault()?.Name ?? string.Empty;

                if (!string.IsNullOrEmpty(entityTableName) &&
                    !string.IsNullOrEmpty(principalTableName) &&
                    !string.IsNullOrEmpty(columnName))
                {
                    foreignKey.SetConstraintName($"fk_{ToSnakeCase(entityTableName)}_{ToSnakeCase(columnName)}_{ToSnakeCase(principalTableName)}");
                }
            }

            foreach (var index in entity.GetIndexes())
            {
                var indexTableName = entity.GetTableName() ?? string.Empty; 
                var columns = string.Join("_", index.Properties.Select(p => ToSnakeCase(p.Name)));

                if (!string.IsNullOrEmpty(indexTableName))
                {
                    index.SetDatabaseName($"ix_{ToSnakeCase(indexTableName)}_{columns}");
                }
            }
        }

        modelBuilder.Entity<AircraftFamily>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Manufacturer).IsRequired().HasMaxLength(100);

            entity.HasMany(e => e.Models)
                  .WithOne(m => m.Family)
                  .HasForeignKey(m => m.FamilyId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AircraftModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Range).IsRequired();
            entity.Property(e => e.PassengerCapacity).IsRequired();
            entity.Property(e => e.CargoCapacity).IsRequired();

            entity.HasOne(e => e.Family)
                  .WithMany(f => f.Models)
                  .HasForeignKey(e => e.FamilyId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Flights)
                  .WithOne(f => f.AircraftModel)
                  .HasForeignKey(f => f.AircraftModelId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(10);
            entity.Property(e => e.DepartureCity).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ArrivalCity).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DepartureDate).IsRequired();
            entity.Property(e => e.ArrivalDate).IsRequired();
            entity.Property(e => e.DepartureTime).IsRequired();
            entity.Property(e => e.FlightDuration).IsRequired();

            entity.HasOne(e => e.AircraftModel)
                  .WithMany(m => m.Flights)
                  .HasForeignKey(e => e.AircraftModelId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Tickets)
                  .WithOne(t => t.Flight)
                  .HasForeignKey(t => t.FlightId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PassportNumber).IsRequired().HasMaxLength(20);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(150);
            entity.Property(e => e.BirthDate).IsRequired();

            entity.HasIndex(e => e.PassportNumber).IsUnique();

            entity.HasMany(e => e.Tickets)
                  .WithOne(t => t.Passenger)
                  .HasForeignKey(t => t.PassengerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SeatNumber).IsRequired().HasMaxLength(5);
            entity.Property(e => e.HasHandLuggage).IsRequired();
            entity.Property(e => e.LuggageWeight).HasPrecision(10, 2);

            entity.HasOne(e => e.Flight)
                  .WithMany(f => f.Tickets)
                  .HasForeignKey(e => e.FlightId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Passenger)
                  .WithMany(p => p.Tickets)
                  .HasForeignKey(e => e.PassengerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        var seeder = new DataSeeder();

        modelBuilder.Entity<AircraftFamily>().HasData(seeder.AircraftFamilies);
        modelBuilder.Entity<AircraftModel>().HasData(seeder.AircraftModels);
        modelBuilder.Entity<Flight>().HasData(seeder.Flights);
        modelBuilder.Entity<Passenger>().HasData(seeder.Passengers);
        modelBuilder.Entity<Ticket>().HasData(seeder.Tickets);
    }

    private static string ToSnakeCase(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var result = new StringBuilder();
        result.Append(char.ToLower(input[0]));

        for (var i = 1; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                result.Append('_');
                result.Append(char.ToLower(input[i]));
            }
            else
            {
                result.Append(input[i]);
            }
        }

        return result.ToString();
    }
}