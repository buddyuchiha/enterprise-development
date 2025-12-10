namespace AviaCompany.Application.Contracts.AircraftFamily;

/// <summary>
/// DTO для POST/PUT запросов к семействам самолетов
/// </summary>
/// <param name="Name">Название семейства</param>
/// <param name="Manufacturer">Производитель</param>
public record AircraftFamilyCreateUpdateDto(
    string? Name,
    string? Manufacturer
);