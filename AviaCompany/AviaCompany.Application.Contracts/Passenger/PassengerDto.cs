namespace AviaCompany.Application.Contracts.Passenger;

/// <summary>
/// DTO для GET запросов к пассажирам
/// </summary>
/// <param name="Id">Идентификатор пассажира</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="FullName">ФИО</param>
/// <param name="BirthDate">Дата рождения</param>
public record PassengerDto(
    int Id,
    string? PassportNumber,
    string? FullName,
    DateTime? BirthDate
);