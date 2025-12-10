namespace AviaCompany.Application.Contracts.Passenger;

/// <summary>
/// DTO для POST/PUT запросов к пассажирам
/// </summary>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="FullName">ФИО</param>
/// <param name="BirthDate">Дата рождения</param>
public record PassengerCreateUpdateDto(
    string? PassportNumber,
    string? FullName,
    DateTime? BirthDate
);