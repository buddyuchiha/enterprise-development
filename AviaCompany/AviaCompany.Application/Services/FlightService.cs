using AutoMapper;
using AviaCompany.Application.Contracts.Flight;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Flights;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с авиарейсами
/// </summary>
public class FlightService(IRepository<Flight, int> repository, IMapper mapper) : IFlightService
{
    /// <summary>
    /// Создает новый авиарейс
    /// </summary>
    /// <param name="dto">DTO для создания или обновления авиарейса</param>
    /// <returns>DTO созданного авиарейса</returns>
    public async Task<FlightDto> Create(FlightCreateUpdateDto dto)
    {
        var flight = mapper.Map<Flight>(dto);
        var result = await repository.Create(flight);
        return mapper.Map<FlightDto>(result);
    }

    /// <summary>
    /// Удаляет авиарейс по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор авиарейса</param>
    /// <returns>Результат удаления (true - успешно, false - неудачно)</returns>
    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    /// <summary>
    /// Получает авиарейс по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор авиарейса</param>
    /// <returns>DTO авиарейса или null, если не найдено</returns>
    public async Task<FlightDto?> Get(int dtoId)
    {
        var flight = await repository.Read(dtoId);
        return flight != null ? mapper.Map<FlightDto>(flight) : null;
    }

    /// <summary>
    /// Получает все авиарейсы
    /// </summary>
    /// <returns>Список DTO всех авиарейсов</returns>
    public async Task<IList<FlightDto>> GetAll()
    {
        var flights = await repository.ReadAll();
        return mapper.Map<IList<FlightDto>>(flights);
    }

    /// <summary>
    /// Получает топ N авиарейсов по количеству пассажиров
    /// </summary>
    /// <param name="count">Количество возвращаемых авиарейсов (по умолчанию 5)</param>
    /// <returns>Список DTO авиарейсов с наибольшим количеством пассажиров</returns>
    public async Task<List<FlightDto>> GetTopFlightsByPassengerCountAsync(int count = 5)
    {
        var flights = await repository.ReadAll();
        var flightsWithPassengerCount = flights
            .Select(f => new
            {
                Flight = f,
                PassengerCount = f.Tickets?.Count ?? 0
            })
            .OrderByDescending(x => x.PassengerCount)
            .Take(count)
            .Select(x => x.Flight)
            .ToList();

        return mapper.Map<List<FlightDto>>(flightsWithPassengerCount);
    }

    /// <summary>
    /// Получает авиарейсы с наименьшей продолжительностью полета
    /// </summary>
    /// <returns>Список DTO авиарейсов с минимальной продолжительностью полета</returns>
    public async Task<List<FlightDto>> GetFlightsWithShortestDurationAsync()
    {
        var flights = await repository.ReadAll();
        if (!flights.Any()) return new List<FlightDto>();

        var minDuration = flights.Min(f => f.FlightDuration);
        var shortestFlights = flights
            .Where(f => f.FlightDuration == minDuration)
            .ToList();

        return mapper.Map<List<FlightDto>>(shortestFlights);
    }

    /// <summary>
    /// Получает авиарейсы по маршруту (городу вылета и городу прилета)
    /// </summary>
    /// <param name="departureCity">Город вылета</param>
    /// <param name="arrivalCity">Город прилета</param>
    /// <returns>Список DTO авиарейсов по указанному маршруту</returns>
    public async Task<List<FlightDto>> GetFlightsByRouteAsync(string departureCity, string arrivalCity)
    {
        var flights = await repository.ReadAll();
        var routeFlights = flights
            .Where(f => f.DepartureCity.Equals(departureCity, StringComparison.OrdinalIgnoreCase) &&
                       f.ArrivalCity.Equals(arrivalCity, StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f.DepartureDate)
            .ThenBy(f => f.DepartureTime)
            .ToList();

        return mapper.Map<List<FlightDto>>(routeFlights);
    }

    /// <summary>
    /// Получает авиарейсы по модели самолета и периоду времени
    /// </summary>
    /// <param name="modelId">Идентификатор модели самолета</param>
    /// <param name="from">Начальная дата периода</param>
    /// <param name="to">Конечная дата периода</param>
    /// <returns>Список DTO авиарейсов, соответствующих критериям</returns>
    public async Task<List<FlightDto>> GetFlightsByModelAndPeriodAsync(int modelId, DateTime from, DateTime to)
    {
        var flights = await repository.ReadAll();
        var filteredFlights = flights
            .Where(f => f.AircraftModelId == modelId &&
                       f.DepartureDate >= from &&
                       f.DepartureDate <= to)
            .ToList();

        return mapper.Map<List<FlightDto>>(filteredFlights);
    }

    /// <summary>
    /// Обновляет авиарейс
    /// </summary>
    /// <param name="dto">DTO для создания или обновления авиарейса</param>
    /// <param name="dtoId">Идентификатор обновляемого авиарейса</param>
    /// <returns>DTO обновленного авиарейса</returns>
    public async Task<FlightDto> Update(FlightCreateUpdateDto dto, int dtoId)
    {
        var flight = mapper.Map<Flight>(dto);
        flight.Id = dtoId;
        var result = await repository.Update(flight);
        return mapper.Map<FlightDto>(result);
    }
}