using AviaCompany.Domain.Models.Flights;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AviaCompany.Domain.Models.Aircrafts;

/// <summary>
/// Модель самолета
/// </summary>
public class AircraftModel
{
    /// <summary>
    /// Идентификатор модели
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Название модели
    /// </summary>
    [StringLength(100)]
    public required string Name { get; set; }

    /// <summary>
    /// Идентификатор семейства
    /// </summary>
    public required int FamilyId { get; set; }

    /// <summary>
    /// Семейство самолета (навигационное свойство)
    /// </summary>
    public virtual AircraftFamily? Family { get; set; }

    /// <summary>
    /// Дальность полета (км)
    /// </summary>
    public required double Range { get; set; }

    /// <summary>
    /// Пассажировместимость
    /// </summary>
    public required int PassengerCapacity { get; set; }

    /// <summary>
    /// Грузовместимость (кг)
    /// </summary>
    public required double CargoCapacity { get; set; }

    /// <summary>
    /// Рейсы этой модели (навигационное свойство)
    /// </summary>
    public virtual List<Flight>? Flights { get; set; }
}