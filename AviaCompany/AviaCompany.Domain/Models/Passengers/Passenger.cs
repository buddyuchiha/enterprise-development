using AviaCompany.Domain.Models.Tickets;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AviaCompany.Domain.Models.Passengers;

/// <summary>
/// Пассажир
/// </summary>
public class Passenger
{
    /// <summary>
    /// Идентификатор пассажира
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    [StringLength(20)]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    [StringLength(200)]
    public required string FullName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Билеты пассажира (навигационное свойство)
    /// </summary>
    public virtual List<Ticket>? Tickets { get; set; }

    public override string ToString() => $"{FullName} ({PassportNumber})";
}