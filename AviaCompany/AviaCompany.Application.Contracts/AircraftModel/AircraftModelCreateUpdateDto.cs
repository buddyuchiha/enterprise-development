namespace AviaCompany.Application.Contracts.AircraftModel;

/// <summary>
/// DTO для POST/PUT запросов к моделям самолетов
/// </summary>
/// <param name="Name">Название модели</param>
/// <param name="FamilyId">Идентификатор семейства</param>
/// <param name="Range">Дальность полета (км)</param>
/// <param name="PassengerCapacity">Пассажировместимость</param>
/// <param name="CargoCapacity">Грузовместимость (кг)</param>
public record AircraftModelCreateUpdateDto(
    string? Name,
    int FamilyId,
    double Range,
    int PassengerCapacity,
    double CargoCapacity
);