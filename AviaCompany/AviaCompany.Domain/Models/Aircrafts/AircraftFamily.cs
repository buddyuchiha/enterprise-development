using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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

    /// <summary>
    /// Модели самолетов этого семейства (навигационное свойство)
    /// </summary>
    public virtual List<AircraftModel>? Models { get; set; }
}