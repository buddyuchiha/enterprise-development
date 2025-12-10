using AviaCompany.Application.Contracts;

namespace AviaCompany.Application.Contracts.AircraftFamily;

/// <summary>
/// Сервис для работы с семействами самолетов
/// </summary>
public interface IAircraftFamilyService : IApplicationService<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, int>
{
}