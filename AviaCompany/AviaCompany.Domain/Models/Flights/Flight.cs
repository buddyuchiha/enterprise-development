using System.ComponentModel.DataAnnotations;

namespace AviaCompany.Domain.Models.Flights;

/// <summary>
/// Авиарейс
/// </summary>
public class Flight
{
    /// <summary>
    /// Идентификатор рейса
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Шифр рейса
    /// </summary>
    [StringLength(10)]
    public string? Code { get; set; }

    /// <summary>
    /// Пункт отправления
    /// </summary>
    [StringLength(100)]
    public required string DepartureCity { get; set; }

    /// <summary>
    /// Пункт прибытия
    /// </summary>
    [StringLength(100)]
    public required string ArrivalCity { get; set; }

    /// <summary>
    /// Дата отправления
    /// </summary>
    public DateTime DepartureDate { get; set; }

    /// <summary>
    /// Дата прибытия
    /// </summary>
    public DateTime ArrivalDate { get; set; }

    /// <summary>
    /// Время отправления
    /// </summary>
    public TimeSpan DepartureTime { get; set; }

    /// <summary>
    /// Время в пути
    /// </summary>
    public TimeSpan FlightDuration { get; set; }

    /// <summary>
    /// Идентификатор модели самолета
    /// </summary>
    public required int AircraftModelId { get; set; }

    public override string ToString() => $"{Code}: {DepartureCity} -> {ArrivalCity}";
}