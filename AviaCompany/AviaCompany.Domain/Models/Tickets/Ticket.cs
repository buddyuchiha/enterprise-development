using System.ComponentModel.DataAnnotations;

namespace AviaCompany.Domain.Models.Tickets;

/// <summary>
/// Билет
/// </summary>
public class Ticket
{
    /// <summary>
    /// Идентификатор билета
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор рейса
    /// </summary>
    public required int FlightId { get; set; }

    /// <summary>
    /// Идентификатор пассажира
    /// </summary>
    public required int PassengerId { get; set; }

    /// <summary>
    /// Номер места
    /// </summary>
    [StringLength(10)]
    public required string SeatNumber { get; set; }

    /// <summary>
    /// Наличие ручной клади
    /// </summary>
    public bool? HasHandLuggage { get; set; }

    /// <summary>
    /// Вес багажа (кг)
    /// </summary>
    public decimal? LuggageWeight { get; set; }

    public override string ToString() => $"Билет {Id}";
}