using Xunit;
using AviaCompany.Domain.Data;
using AviaCompany.Domain.Models.Aircrafts;
using AviaCompany.Domain.Models.Flights;
using AviaCompany.Domain.Models.Passengers;
using AviaCompany.Domain.Models.Tickets;

namespace AviaCompany.Tests;

/// <summary>
/// ����� ��� �������� ������������� �������� ������������ �������� ��
/// </summary>
public class FlightTests
{
    private readonly List<Flight> _flights;
    private readonly List<Passenger> _passengers;
    private readonly List<Ticket> _tickets;
    private readonly List<AircraftModel> _aircraftModels;

    public FlightTests()
    {
        _flights = DataSeeder.Flights;
        _passengers = DataSeeder.Passengers;
        _tickets = DataSeeder.Tickets;
        _aircraftModels = DataSeeder.AircraftModels;
    }

    /// <summary>
    /// ���� 1: ������� ��� 5 ���������� �� ���������� ������������ ����������
    /// </summary>
    [Fact]
    public void Top5FlightsByPassengerCount_ReturnsCorrectResults()
    {
        var topFlights = _flights
            .Select(flight => new
            {
                Flight = flight,
                PassengerCount = _tickets.Count(ticket => ticket.FlightId == flight.Id)
            })
            .OrderByDescending(x => x.PassengerCount)
            .Take(5)
            .ToList();

        Assert.NotNull(topFlights);
        Assert.InRange(topFlights.Count, 0, 5);

        for (int i = 0; i < topFlights.Count - 1; i++)
        {
            Assert.True(topFlights[i].PassengerCount >= topFlights[i + 1].PassengerCount);
        }

        if (topFlights.Any())
        {
            var maxPassengerCount = _flights.Max(f => _tickets.Count(t => t.FlightId == f.Id));
            Assert.Contains(topFlights, x => x.PassengerCount == maxPassengerCount);
        }
    }

    /// <summary>
    /// ���� 2: ������� ������ ������ � ����������� �������� � ����
    /// </summary>
    [Fact]
    public void FlightsWithMinimalDuration_ReturnsShortestFlights()
    {
        // Arrange & Act
        var minDuration = _flights.Min(f => f.FlightDuration);
        var shortestFlights = _flights
            .Where(f => f.FlightDuration == minDuration)
            .ToList();

        // Assert
        Assert.NotNull(shortestFlights);

        if (shortestFlights.Any())
        {
            // ��� ��������� ����� ������ ����� ����������� ������������
            Assert.All(shortestFlights, flight =>
                Assert.Equal(minDuration, flight.FlightDuration));

            // ����������, ��� ��� ������ � ������� �������������
            Assert.DoesNotContain(_flights, f => f.FlightDuration < minDuration);
        }
    }

    /// <summary>
    /// ���� 3: ������� �������� ��� ���� ����������, ������� ��������� ������, 
    /// ��� ������ ������� ����� ����, ����������� �� ���
    /// </summary>
    [Theory]
    [InlineData(1)]  
    [InlineData(2)] 
    [InlineData(3)] 
    public void PassengersWithZeroLuggage_ReturnsOrderedList(int flightId)
    {
        var flightExists = _flights.Any(f => f.Id == flightId);
        Assert.True(flightExists, $"���� � ID {flightId} �� ������");

        var passengersWithNoLuggage = (
            from ticket in _tickets
            where ticket.FlightId == flightId && ticket.LuggageWeight == 0
            join passenger in _passengers on ticket.PassengerId equals passenger.Id
            orderby passenger.FullName
            select passenger
        ).ToList();

        Assert.NotNull(passengersWithNoLuggage);

        Assert.All(passengersWithNoLuggage, passenger =>
        {
            var ticket = _tickets.First(t =>
                t.FlightId == flightId && t.PassengerId == passenger.Id);
            Assert.Equal(0, ticket.LuggageWeight);
        });

        var expectedOrder = passengersWithNoLuggage
            .Select(p => p.FullName)
            .OrderBy(name => name)
            .ToList();
        var actualOrder = passengersWithNoLuggage
            .Select(p => p.FullName)
            .ToList();

        Assert.Equal(expectedOrder, actualOrder);
    }

    /// <summary>
    /// ���� 4: ������� ������� ���������� ��� ���� ������� ��������� ��������� ������ � ��������� ������ �������
    /// </summary>
    [Fact]
    public void FlightsByModelAndPeriod_ReturnsConsolidatedInfo()
    {
        var modelId = 1; 
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 31);

        var modelExists = _aircraftModels.Any(m => m.Id == modelId);
        Assert.True(modelExists, $"������ �������� � ID {modelId} �� �������");

        var flightsInfo = (
            from flight in _flights
            where flight.AircraftModelId == modelId
                  && flight.DepartureDate >= startDate
                  && flight.DepartureDate <= endDate
            let passengerCount = _tickets.Count(t => t.FlightId == flight.Id)
            select new
            {
                FlightInfo = flight,
                PassengerCount = passengerCount,
                TotalLuggageWeight = _tickets
                    .Where(t => t.FlightId == flight.Id)
                    .Sum(t => t.LuggageWeight)
            }
        ).ToList();

        Assert.NotNull(flightsInfo);

        Assert.All(flightsInfo, info =>
        {
            Assert.Equal(modelId, info.FlightInfo.AircraftModelId);
            Assert.InRange(info.FlightInfo.DepartureDate, startDate, endDate);
            Assert.True(info.PassengerCount >= 0);
            Assert.True(info.TotalLuggageWeight >= 0);
        });
    }

    /// <summary>
    /// ���� 5: ������� �������� � ���� ����������, ���������� �� ���������� ������ ����������� � ��������� ����� ��������
    /// </summary>
    [Theory]
    [InlineData("������", "�����-���������")]
    [InlineData("�����-���������", "������")]
    [InlineData("������", "����")]
    public void FlightsByRoute_ReturnsCorrectFlights(string departureCity, string arrivalCity)
    {
        var routeFlights = _flights
            .Where(f => f.DepartureCity == departureCity && f.ArrivalCity == arrivalCity)
            .OrderBy(f => f.DepartureDate)
            .ThenBy(f => f.DepartureTime)
            .ToList();

        Assert.NotNull(routeFlights);

        Assert.All(routeFlights, flight =>
        {
            Assert.Equal(departureCity, flight.DepartureCity);
            Assert.Equal(arrivalCity, flight.ArrivalCity);
        });

        for (int i = 0; i < routeFlights.Count - 1; i++)
        {
            var current = routeFlights[i];
            var next = routeFlights[i + 1];

            Assert.True(current.DepartureDate <= next.DepartureDate);
            if (current.DepartureDate == next.DepartureDate)
            {
                Assert.True(current.DepartureTime <= next.DepartureTime);
            }
        }
    }
}