using AviaCompany.Domain.Models;
using AviaCompany.Domain.Models.Aircrafts;
using AviaCompany.Domain.Models.Flights;
using AviaCompany.Domain.Models.Passengers;
using AviaCompany.Domain.Models.Tickets;

namespace AviaCompany.Domain.Data;

/// <summary>
/// Класс для первичного наполнения коллекций данными
/// </summary>
/// <remarks>
/// Данные сгенерированы для тестирования функциональности авиакомпании
/// </remarks>
public static class DataSeeder
{
    public static readonly List<AircraftFamily> AircraftFamilies = [
        new AircraftFamily
        {
            Id = 1,
            Name = "A320",
            Manufacturer = "Airbus"
        },
        new AircraftFamily
        {
            Id = 2,
            Name = "737",
            Manufacturer = "Boeing"
        },
        new AircraftFamily
        {
            Id = 3,
            Name = "Superjet 100",
            Manufacturer = "Sukhoi"
        },
        new AircraftFamily
        {
            Id = 4,
            Name = "MS-21",
            Manufacturer = "Irkut"
        },
        new AircraftFamily
        {
            Id = 5,
            Name = "CRJ",
            Manufacturer = "Bombardier"
        },
        new AircraftFamily
        {
            Id = 6,
            Name = "A330",
            Manufacturer = "Airbus"
        },
        new AircraftFamily
        {
            Id = 7,
            Name = "777",
            Manufacturer = "Boeing"
        },
        new AircraftFamily
        {
            Id = 8,
            Name = "A350",
            Manufacturer = "Airbus"
        },
        new AircraftFamily
        {
            Id = 9,
            Name = "787",
            Manufacturer = "Boeing"
        },
        new AircraftFamily
        {
            Id = 10,
            Name = "Embraer E-Jet",
            Manufacturer = "Embraer"
        }
    ];

    public static readonly List<AircraftModel> AircraftModels = [
        new AircraftModel
        {
            Id = 1,
            Name = "A320-200",
            FamilyId = 1,
            Range = 6100,
            PassengerCapacity = 180,
            CargoCapacity = 20000
        },
        new AircraftModel
        {
            Id = 2,
            Name = "A321neo",
            FamilyId = 1,
            Range = 7400,
            PassengerCapacity = 240,
            CargoCapacity = 25000
        },
        new AircraftModel
        {
            Id = 3,
            Name = "737-800",
            FamilyId = 2,
            Range = 5765,
            PassengerCapacity = 189,
            CargoCapacity = 22000
        },
        new AircraftModel
        {
            Id = 4,
            Name = "737 MAX 8",
            FamilyId = 2,
            Range = 6570,
            PassengerCapacity = 210,
            CargoCapacity = 23000
        },
        new AircraftModel
        {
            Id = 5,
            Name = "SSJ-100",
            FamilyId = 3,
            Range = 3048,
            PassengerCapacity = 98,
            CargoCapacity = 12000
        },
        new AircraftModel
        {
            Id = 6,
            Name = "MS-21-300",
            FamilyId = 4,
            Range = 6000,
            PassengerCapacity = 211,
            CargoCapacity = 24000
        },
        new AircraftModel
        {
            Id = 7,
            Name = "CRJ-200",
            FamilyId = 5,
            Range = 3045,
            PassengerCapacity = 50,
            CargoCapacity = 8000
        },
        new AircraftModel
        {
            Id = 8,
            Name = "A319",
            FamilyId = 1,
            Range = 6850,
            PassengerCapacity = 160,
            CargoCapacity = 18000
        },
        new AircraftModel
        {
            Id = 9,
            Name = "737-700",
            FamilyId = 2,
            Range = 6230,
            PassengerCapacity = 149,
            CargoCapacity = 19000
        },
        new AircraftModel
        {
            Id = 10,
            Name = "CRJ-900",
            FamilyId = 5,
            Range = 3400,
            PassengerCapacity = 90,
            CargoCapacity = 15000
        },
        new AircraftModel
        {
            Id = 11,
            Name = "A330-300",
            FamilyId = 6,
            Range = 10800,
            PassengerCapacity = 440,
            CargoCapacity = 70000
        },
        new AircraftModel
        {
            Id = 12,
            Name = "777-300ER",
            FamilyId = 7,
            Range = 13650,
            PassengerCapacity = 550,
            CargoCapacity = 75000
        },
        new AircraftModel
        {
            Id = 13,
            Name = "A350-900",
            FamilyId = 8,
            Range = 15000,
            PassengerCapacity = 440,
            CargoCapacity = 68000
        },
        new AircraftModel
        {
            Id = 14,
            Name = "787-9 Dreamliner",
            FamilyId = 9,
            Range = 14140,
            PassengerCapacity = 420,
            CargoCapacity = 65000
        },
        new AircraftModel
        {
            Id = 15,
            Name = "Embraer 195",
            FamilyId = 10,
            Range = 4260,
            PassengerCapacity = 132,
            CargoCapacity = 16000
        }
    ];

    public static readonly List<Flight> Flights = [
        new Flight
        {
            Id = 1,
            Code = "SU1001",
            DepartureCity = "Москва",
            ArrivalCity = "Санкт-Петербург",
            DepartureDate = new DateTime(2024, 1, 15),
            ArrivalDate = new DateTime(2024, 1, 15),
            DepartureTime = new TimeSpan(8, 0, 0),
            FlightDuration = new TimeSpan(1, 30, 0),
            AircraftModelId = 1
        },
        new Flight
        {
            Id = 2,
            Code = "SU1002",
            DepartureCity = "Москва",
            ArrivalCity = "Сочи",
            DepartureDate = new DateTime(2024, 1, 15),
            ArrivalDate = new DateTime(2024, 1, 15),
            DepartureTime = new TimeSpan(10, 30, 0),
            FlightDuration = new TimeSpan(2, 30, 0),
            AircraftModelId = 2
        },
        new Flight
        {
            Id = 3,
            Code = "SU1003",
            DepartureCity = "Москва",
            ArrivalCity = "Екатеринбург",
            DepartureDate = new DateTime(2024, 1, 16),
            ArrivalDate = new DateTime(2024, 1, 16),
            DepartureTime = new TimeSpan(12, 0, 0),
            FlightDuration = new TimeSpan(2, 15, 0),
            AircraftModelId = 3
        },
        new Flight
        {
            Id = 4,
            Code = "SU1004",
            DepartureCity = "Москва",
            ArrivalCity = "Новосибирск",
            DepartureDate = new DateTime(2024, 1, 16),
            ArrivalDate = new DateTime(2024, 1, 16),
            DepartureTime = new TimeSpan(14, 30, 0),
            FlightDuration = new TimeSpan(4, 0, 0),
            AircraftModelId = 4
        },
        new Flight
        {
            Id = 5,
            Code = "SU1005",
            DepartureCity = "Москва",
            ArrivalCity = "Владивосток",
            DepartureDate = new DateTime(2024, 1, 17),
            ArrivalDate = new DateTime(2024, 1, 17),
            DepartureTime = new TimeSpan(16, 0, 0),
            FlightDuration = new TimeSpan(8, 30, 0),
            AircraftModelId = 6
        },
        new Flight
        {
            Id = 6,
            Code = "SU1006",
            DepartureCity = "Санкт-Петербург",
            ArrivalCity = "Москва",
            DepartureDate = new DateTime(2024, 1, 15),
            ArrivalDate = new DateTime(2024, 1, 15),
            DepartureTime = new TimeSpan(19, 0, 0),
            FlightDuration = new TimeSpan(1, 30, 0),
            AircraftModelId = 1
        },
        new Flight
        {
            Id = 7,
            Code = "SU1007",
            DepartureCity = "Санкт-Петербург",
            ArrivalCity = "Сочи",
            DepartureDate = new DateTime(2024, 1, 16),
            ArrivalDate = new DateTime(2024, 1, 16),
            DepartureTime = new TimeSpan(9, 0, 0),
            FlightDuration = new TimeSpan(3, 0, 0),
            AircraftModelId = 5
        },
        new Flight
        {
            Id = 8,
            Code = "SU1008",
            DepartureCity = "Сочи",
            ArrivalCity = "Москва",
            DepartureDate = new DateTime(2024, 1, 16),
            ArrivalDate = new DateTime(2024, 1, 16),
            DepartureTime = new TimeSpan(20, 0, 0),
            FlightDuration = new TimeSpan(2, 30, 0),
            AircraftModelId = 2
        },
        new Flight
        {
            Id = 9,
            Code = "SU1009",
            DepartureCity = "Екатеринбург",
            ArrivalCity = "Москва",
            DepartureDate = new DateTime(2024, 1, 17),
            ArrivalDate = new DateTime(2024, 1, 17),
            DepartureTime = new TimeSpan(7, 30, 0),
            FlightDuration = new TimeSpan(2, 15, 0),
            AircraftModelId = 3
        },
        new Flight
        {
            Id = 10,
            Code = "SU1010",
            DepartureCity = "Новосибирск",
            ArrivalCity = "Санкт-Петербург",
            DepartureDate = new DateTime(2024, 1, 17),
            ArrivalDate = new DateTime(2024, 1, 17),
            DepartureTime = new TimeSpan(11, 0, 0),
            FlightDuration = new TimeSpan(4, 30, 0),
            AircraftModelId = 7
        },
        new Flight
        {
            Id = 11,
            Code = "SU1011",
            DepartureCity = "Москва",
            ArrivalCity = "Калининград",
            DepartureDate = new DateTime(2024, 1, 18),
            ArrivalDate = new DateTime(2024, 1, 18),
            DepartureTime = new TimeSpan(13, 0, 0),
            FlightDuration = new TimeSpan(2, 0, 0),
            AircraftModelId = 8
        },
        new Flight
        {
            Id = 12,
            Code = "SU1012",
            DepartureCity = "Калининград",
            ArrivalCity = "Москва",
            DepartureDate = new DateTime(2024, 1, 18),
            ArrivalDate = new DateTime(2024, 1, 18),
            DepartureTime = new TimeSpan(17, 0, 0),
            FlightDuration = new TimeSpan(2, 0, 0),
            AircraftModelId = 9
        },
        new Flight
        {
            Id = 13,
            Code = "SU1013",
            DepartureCity = "Москва",
            ArrivalCity = "Париж",
            DepartureDate = new DateTime(2024, 1, 19),
            ArrivalDate = new DateTime(2024, 1, 19),
            DepartureTime = new TimeSpan(10, 0, 0),
            FlightDuration = new TimeSpan(4, 0, 0),
            AircraftModelId = 11
        },
        new Flight
        {
            Id = 14,
            Code = "SU1014",
            DepartureCity = "Москва",
            ArrivalCity = "Нью-Йорк",
            DepartureDate = new DateTime(2024, 1, 20),
            ArrivalDate = new DateTime(2024, 1, 20),
            DepartureTime = new TimeSpan(14, 0, 0),
            FlightDuration = new TimeSpan(10, 30, 0),
            AircraftModelId = 12
        },
        new Flight
        {
            Id = 15,
            Code = "SU1015",
            DepartureCity = "Москва",
            ArrivalCity = "Токио",
            DepartureDate = new DateTime(2024, 1, 21),
            ArrivalDate = new DateTime(2024, 1, 21),
            DepartureTime = new TimeSpan(16, 0, 0),
            FlightDuration = new TimeSpan(9, 45, 0),
            AircraftModelId = 13
        }
    ];

    public static readonly List<Passenger> Passengers = [
        new Passenger
        {
            Id = 1,
            PassportNumber = "45 01 123456",
            FullName = "Иванов Иван Иванович",
            BirthDate = new DateTime(1985, 5, 15)
        },
        new Passenger
        {
            Id = 2,
            PassportNumber = "45 02 234567",
            FullName = "Петров Петр Петрович",
            BirthDate = new DateTime(1990, 8, 22)
        },
        new Passenger
        {
            Id = 3,
            PassportNumber = "45 03 345678",
            FullName = "Сидорова Анна Владимировна",
            BirthDate = new DateTime(1988, 3, 10)
        },
        new Passenger
        {
            Id = 4,
            PassportNumber = "45 04 456789",
            FullName = "Козлов Дмитрий Сергеевич",
            BirthDate = new DateTime(1992, 11, 5)
        },
        new Passenger
        {
            Id = 5,
            PassportNumber = "45 05 567890",
            FullName = "Николаева Екатерина Андреевна",
            BirthDate = new DateTime(1987, 7, 30)
        },
        new Passenger
        {
            Id = 6,
            PassportNumber = "45 06 678901",
            FullName = "Федоров Алексей Викторович",
            BirthDate = new DateTime(1995, 2, 14)
        },
        new Passenger
        {
            Id = 7,
            PassportNumber = "45 07 789012",
            FullName = "Морозова Ольга Дмитриевна",
            BirthDate = new DateTime(1991, 9, 18)
        },
        new Passenger
        {
            Id = 8,
            PassportNumber = "45 08 890123",
            FullName = "Волков Сергей Иванович",
            BirthDate = new DateTime(1983, 12, 3)
        },
        new Passenger
        {
            Id = 9,
            PassportNumber = "45 09 901234",
            FullName = "Павлова Мария Петровна",
            BirthDate = new DateTime(1993, 6, 25)
        },
        new Passenger
        {
            Id = 10,
            PassportNumber = "45 10 012345",
            FullName = "Семенов Андрей Николаевич",
            BirthDate = new DateTime(1989, 4, 8)
        },
        new Passenger
        {
            Id = 11,
            PassportNumber = "45 11 112233",
            FullName = "Орлова Татьяна Владимировна",
            BirthDate = new DateTime(1994, 1, 20)
        },
        new Passenger
        {
            Id = 12,
            PassportNumber = "45 12 223344",
            FullName = "Белов Игорь Александрович",
            BirthDate = new DateTime(1986, 10, 12)
        },
        new Passenger
        {
            Id = 13,
            PassportNumber = "45 13 334455",
            FullName = "Киселева Надежда Сергеевна",
            BirthDate = new DateTime(1996, 3, 7)
        },
        new Passenger
        {
            Id = 14,
            PassportNumber = "45 14 445566",
            FullName = "Григорьев Павел Олегович",
            BirthDate = new DateTime(1984, 8, 28)
        },
        new Passenger
        {
            Id = 15,
            PassportNumber = "45 15 556677",
            FullName = "Титова Юлия Игоревна",
            BirthDate = new DateTime(1997, 5, 15)
        },
        new Passenger
        {
            Id = 16,
            PassportNumber = "45 16 667788",
            FullName = "Александров Максим Викторович",
            BirthDate = new DateTime(1982, 7, 8)
        },
        new Passenger
        {
            Id = 17,
            PassportNumber = "45 17 778899",
            FullName = "Васильева Елена Сергеевна",
            BirthDate = new DateTime(1998, 11, 25)
        },
        new Passenger
        {
            Id = 18,
            PassportNumber = "45 18 889900",
            FullName = "Михайлов Артем Игоревич",
            BirthDate = new DateTime(1981, 4, 17)
        },
        new Passenger
        {
            Id = 19,
            PassportNumber = "45 19 990011",
            FullName = "Романова Светлана Александровна",
            BirthDate = new DateTime(1999, 2, 28)
        },
        new Passenger
        {
            Id = 20,
            PassportNumber = "45 20 001122",
            FullName = "Данилов Владислав Олегович",
            BirthDate = new DateTime(1980, 12, 10)
        }
    ];

    public static readonly List<Ticket> Tickets = [
        new Ticket { Id = 1, FlightId = 1, PassengerId = 1, SeatNumber = "10A", HasHandLuggage = true, LuggageWeight = 15.5m },
        new Ticket { Id = 2, FlightId = 1, PassengerId = 2, SeatNumber = "10B", HasHandLuggage = true, LuggageWeight = 10.0m },
        new Ticket { Id = 3, FlightId = 1, PassengerId = 3, SeatNumber = "11A", HasHandLuggage = false, LuggageWeight = 0m },
        new Ticket { Id = 4, FlightId = 1, PassengerId = 4, SeatNumber = "11B", HasHandLuggage = true, LuggageWeight = 8.0m },
        new Ticket { Id = 5, FlightId = 1, PassengerId = 5, SeatNumber = "12A", HasHandLuggage = true, LuggageWeight = 0m },
        new Ticket { Id = 6, FlightId = 1, PassengerId = 6, SeatNumber = "12B", HasHandLuggage = true, LuggageWeight = 12.0m },
        new Ticket { Id = 7, FlightId = 1, PassengerId = 7, SeatNumber = "13A", HasHandLuggage = false, LuggageWeight = 0m },
        new Ticket { Id = 8, FlightId = 1, PassengerId = 8, SeatNumber = "13B", HasHandLuggage = true, LuggageWeight = 9.5m },

        new Ticket { Id = 9, FlightId = 2, PassengerId = 9, SeatNumber = "15C", HasHandLuggage = true, LuggageWeight = 20.0m },
        new Ticket { Id = 10, FlightId = 2, PassengerId = 10, SeatNumber = "15D", HasHandLuggage = true, LuggageWeight = 12.5m },
        new Ticket { Id = 11, FlightId = 2, PassengerId = 11, SeatNumber = "16C", HasHandLuggage = false, LuggageWeight = 0m },
        new Ticket { Id = 12, FlightId = 2, PassengerId = 12, SeatNumber = "16D", HasHandLuggage = true, LuggageWeight = 0m },
        new Ticket { Id = 13, FlightId = 2, PassengerId = 13, SeatNumber = "17C", HasHandLuggage = true, LuggageWeight = 18.0m },
        new Ticket { Id = 14, FlightId = 2, PassengerId = 14, SeatNumber = "17D", HasHandLuggage = true, LuggageWeight = 9.5m },
        new Ticket { Id = 15, FlightId = 2, PassengerId = 15, SeatNumber = "18C", HasHandLuggage = false, LuggageWeight = 0m },
        new Ticket { Id = 16, FlightId = 2, PassengerId = 16, SeatNumber = "18D", HasHandLuggage = true, LuggageWeight = 14.0m },
        new Ticket { Id = 17, FlightId = 2, PassengerId = 17, SeatNumber = "19C", HasHandLuggage = true, LuggageWeight = 11.0m },
        new Ticket { Id = 18, FlightId = 2, PassengerId = 18, SeatNumber = "19D", HasHandLuggage = false, LuggageWeight = 0m },

        new Ticket { Id = 19, FlightId = 3, PassengerId = 19, SeatNumber = "5A", HasHandLuggage = true, LuggageWeight = 11.0m },
        new Ticket { Id = 20, FlightId = 3, PassengerId = 20, SeatNumber = "5B", HasHandLuggage = true, LuggageWeight = 0m },
        new Ticket { Id = 21, FlightId = 3, PassengerId = 1, SeatNumber = "6A", HasHandLuggage = false, LuggageWeight = 0m },
        new Ticket { Id = 22, FlightId = 3, PassengerId = 3, SeatNumber = "6B", HasHandLuggage = true, LuggageWeight = 16.5m },
        new Ticket { Id = 23, FlightId = 3, PassengerId = 5, SeatNumber = "7A", HasHandLuggage = true, LuggageWeight = 7.0m },
        new Ticket { Id = 24, FlightId = 3, PassengerId = 7, SeatNumber = "7B", HasHandLuggage = true, LuggageWeight = 0m },
        new Ticket { Id = 25, FlightId = 3, PassengerId = 9, SeatNumber = "8A", HasHandLuggage = false, LuggageWeight = 0m },

        new Ticket { Id = 26, FlightId = 4, PassengerId = 2, SeatNumber = "20A", HasHandLuggage = true, LuggageWeight = 13.0m },
        new Ticket { Id = 27, FlightId = 4, PassengerId = 4, SeatNumber = "20B", HasHandLuggage = true, LuggageWeight = 0m },
        new Ticket { Id = 28, FlightId = 5, PassengerId = 6, SeatNumber = "25C", HasHandLuggage = true, LuggageWeight = 22.0m },
        new Ticket { Id = 29, FlightId = 6, PassengerId = 8, SeatNumber = "8A", HasHandLuggage = false, LuggageWeight = 0m },
        new Ticket { Id = 30, FlightId = 6, PassengerId = 10, SeatNumber = "8B", HasHandLuggage = true, LuggageWeight = 10.5m },
        new Ticket { Id = 31, FlightId = 7, PassengerId = 12, SeatNumber = "12C", HasHandLuggage = true, LuggageWeight = 0m },
        new Ticket { Id = 32, FlightId = 8, PassengerId = 14, SeatNumber = "14A", HasHandLuggage = true, LuggageWeight = 15.0m },
        new Ticket { Id = 33, FlightId = 9, PassengerId = 16, SeatNumber = "9B", HasHandLuggage = false, LuggageWeight = 0m },
        new Ticket { Id = 34, FlightId = 10, PassengerId = 18, SeatNumber = "11C", HasHandLuggage = true, LuggageWeight = 8.5m },
        new Ticket { Id = 35, FlightId = 11, PassengerId = 20, SeatNumber = "7D", HasHandLuggage = true, LuggageWeight = 0m },
        new Ticket { Id = 36, FlightId = 12, PassengerId = 2, SeatNumber = "13A", HasHandLuggage = true, LuggageWeight = 12.0m },
        new Ticket { Id = 37, FlightId = 13, PassengerId = 4, SeatNumber = "21B", HasHandLuggage = true, LuggageWeight = 25.0m },
        new Ticket { Id = 38, FlightId = 14, PassengerId = 6, SeatNumber = "35C", HasHandLuggage = true, LuggageWeight = 30.0m },
        new Ticket { Id = 39, FlightId = 15, PassengerId = 8, SeatNumber = "42A", HasHandLuggage = false, LuggageWeight = 0m },
        new Ticket { Id = 40, FlightId = 15, PassengerId = 10, SeatNumber = "42B", HasHandLuggage = true, LuggageWeight = 18.5m }
    ];
}