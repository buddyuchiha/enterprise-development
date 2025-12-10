using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AviaCompany.Application.Contracts;

namespace AviaCompany.Application.Contracts.Flight;

/// <summary>
/// Сервис для работы с авиарейсами (CRUD + аналитика)
/// </summary>
public interface IFlightService : IApplicationService<FlightDto, FlightCreateUpdateDto, int>
{
    /// <summary>
    /// Получает топ-N авиарейсов с наибольшим количеством пассажиров
    /// </summary>
    /// <param name="count">Количество рейсов для возврата (по умолчанию — 5)</param>
    /// <returns>Список DTO рейсов, отсортированных по убыванию числа пассажиров</returns>
    public Task<List<FlightDto>> GetTopFlightsByPassengerCountAsync(int count = 5);

    /// <summary>
    /// Получает все авиарейсы с минимальным временем в пути
    /// </summary>
    /// <returns>Список DTO рейсов с наименьшим временем полёта</returns>
    public Task<List<FlightDto>> GetFlightsWithShortestDurationAsync();

    /// <summary>
    /// Получает все авиарейсы по указанному маршруту
    /// </summary>
    /// <param name="departureCity">Город отправления</param>
    /// <param name="arrivalCity">Город прибытия</param>
    /// <returns>Список DTO рейсов по маршруту</returns>
    public Task<List<FlightDto>> GetFlightsByRouteAsync(string departureCity, string arrivalCity);
}
