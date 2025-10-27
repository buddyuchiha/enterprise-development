using Xunit;
using AviaCompany.Domain.Data;

namespace AviaCompany.Tests;

/// <summary>
/// Набор тестов для проверки аналитических запросов авиакомпании
/// </summary>
public class FlightTests(DataSeeder data) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Проверяет, что рейсы с минимальной длительностью корректно определяются
    /// </summary>
    [Fact]
    public void GetFlightsWithMinimalDuration_ReturnsExpectedFlights()
    {
        var minDuration = data.Flights.Min(f => f.FlightDuration);
        var shortestFlights = data.Flights.Where(f => f.FlightDuration == minDuration).ToList();
        Assert.NotEmpty(shortestFlights);
        Assert.Contains(shortestFlights, f => f.FlightDuration == minDuration);
    }

    /// <summary>
    /// Проверяет, что топ-5 авиарейсов по количеству пассажиров определяются корректно
    /// </summary>
    [Fact]
    public void GetTop5FlightsByPassengerCount_ReturnsFiveFlights()
    {
        var topFlights = data.Flights.Select(f => new { Flight = f, PassengerCount = data.Tickets.Count(t => t.FlightId == f.Id) })
            .OrderByDescending(x => x.PassengerCount).Take(5).ToList();
        Assert.NotEmpty(topFlights);
        Assert.True(topFlights.Count <= 5);
        var isOrdered = topFlights.SequenceEqual(topFlights.OrderByDescending(x => x.PassengerCount));
        Assert.True(isOrdered, "Рейсы должны быть отсортированы по убыванию количества пассажиров");
    }

    /// <summary>
    /// Проверяет, что пассажиры без багажа корректно определяются для выбранных рейсов
    /// </summary>
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetPassengersWithoutBaggage_ReturnsPassengers(int flightId)
    {
        var flight = data.Flights.FirstOrDefault(f => f.Id == flightId);
        Assert.NotNull(flight);
        var passengers = data.Tickets.Where(t => t.FlightId == flightId && t.LuggageWeight == 0)
            .Select(t => data.Passengers.First(p => p.Id == t.PassengerId)).OrderBy(p => p.FullName).ToList();
        Assert.NotNull(passengers);
        var isOrdered = passengers.SequenceEqual(passengers.OrderBy(p => p.FullName));
        Assert.True(isOrdered, "Пассажиры должны быть отсортированы по ФИО");
    }

    /// <summary>
    /// Проверяет, что полеты выбранной модели за указанный период времени корректно определяются
    /// </summary>
    [Fact]
    public void GetFlightsByModelAndPeriod_ReturnsCorrectFlights()
    {
        var model = data.AircraftModels.First();
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 31);
        var flights = data.Flights.Where(f => f.AircraftModelId == model.Id && f.DepartureDate >= startDate && f.DepartureDate <= endDate).ToList();

        Assert.NotEmpty(flights);
        Assert.All(flights, flight => Assert.Equal(model.Id, flight.AircraftModelId));
        Assert.All(flights, flight => Assert.True(flight.DepartureDate >= startDate && flight.DepartureDate <= endDate));
    }

    /// <summary>
    /// Проверяет, что рейсы из заданного пункта отправления в пункт прибытия корректно определяются
    /// </summary>
    [Fact]
    public void GetFlightsByRoute_ReturnsExpectedFlights()
    {
        var departureCity = "Москва";
        var arrivalCity = "Санкт-Петербург";
        var flights = data.Flights.Where(f => f.DepartureCity == departureCity && f.ArrivalCity == arrivalCity)
            .OrderBy(f => f.DepartureDate).ToList();
        Assert.NotEmpty(flights);
        Assert.All(flights, flight => Assert.Equal(departureCity, flight.DepartureCity));
        Assert.All(flights, flight => Assert.Equal(arrivalCity, flight.ArrivalCity));
        var isOrdered = flights.SequenceEqual(flights.OrderBy(f => f.DepartureDate));
        Assert.True(isOrdered, "Рейсы должны быть отсортированы по дате отправления");
    }

    /// <summary>
    /// Проверяет, что тестовые данные загружены корректно
    /// </summary>
    [Fact]
    public void FixtureContainsTestData()
    {
        Assert.NotEmpty(data.Flights);
        Assert.NotEmpty(data.Passengers);
        Assert.NotEmpty(data.Tickets);
        Assert.NotEmpty(data.AircraftModels);
        Assert.NotEmpty(data.AircraftFamilies);
    }
}