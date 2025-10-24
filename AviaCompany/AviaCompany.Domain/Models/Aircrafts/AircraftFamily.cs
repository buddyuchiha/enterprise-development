using System.ComponentModel.DataAnnotations;

namespace AviaCompany.Domain.Models.Aircrafts;

/// <summary>
/// Семейство самолетов
/// </summary>
public class AircraftFamily
{
    /// <summary>
    /// Идентификатор семейства
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Название семейства
    /// </summary>
    [StringLength(100)]
    public required string Name { get; set; }

    /// <summary>
    /// Производитель
    /// </summary>
    [StringLength(100)]
    public required string Manufacturer { get; set; }
}