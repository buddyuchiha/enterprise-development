namespace AviaCompany.Application.Contracts.AircraftFamily;

/// <summary>
/// DTO для GET запросов к семействам самолетов
/// </summary>
/// <param name="Id">Идентификатор семейства</param>
/// <param name="Name">Название семейства</param>
/// <param name="Manufacturer">Производитель</param>
public record AircraftFamilyDto(
    int Id,
    string? Name,
    string? Manufacturer
);