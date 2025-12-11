using AutoMapper;
using AviaCompany.Application.Contracts.AircraftFamily;
using AviaCompany.Application.Contracts.AircraftModel;
using AviaCompany.Application.Contracts.Flight;
using AviaCompany.Application.Contracts.Passenger;
using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Domain.Models.Aircrafts;
using AviaCompany.Domain.Models.Flights;
using AviaCompany.Domain.Models.Passengers;
using AviaCompany.Domain.Models.Tickets;

namespace AviaCompany.Application.Mappings;

/// <summary>
/// Профиль AutoMapper для маппинга между сущностями и DTO
/// </summary>
public class AviaCompanyProfile : Profile
{
    public AviaCompanyProfile()
    {
        CreateMap<AircraftFamily, AircraftFamilyDto>();
        CreateMap<AircraftFamilyCreateUpdateDto, AircraftFamily>();

        CreateMap<AircraftModel, AircraftModelDto>();
        CreateMap<AircraftModelCreateUpdateDto, AircraftModel>();

        CreateMap<Flight, FlightDto>();
        CreateMap<FlightCreateUpdateDto, Flight>();

        CreateMap<Passenger, PassengerDto>();
        CreateMap<PassengerCreateUpdateDto, Passenger>();

        CreateMap<Ticket, TicketDto>();
        CreateMap<TicketCreateUpdateDto, Ticket>();
    }
}