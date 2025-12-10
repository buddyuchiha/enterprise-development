namespace AviaCompany.Application.Contracts.AircraftModel;

/// <summary>
/// DTO для GET запросов к моделям самолетов
/// </summary>
/// <param name="Id">Идентификатор модели</param>
/// <param name="Name">Название модели</param>
/// <param name="FamilyId">Идентификатор семейства</param>
/// <param name="Range">Дальность полета (км)</param>
/// <param name="PassengerCapacity">Пассажировместимость</param>
/// <param name="CargoCapacity">Грузовместимость (кг)</param>
public record AircraftModelDto(
    int Id,
    string? Name,
    int FamilyId,
    double Range,
    int PassengerCapacity,
    double CargoCapacity
);