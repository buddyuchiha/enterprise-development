using AutoMapper;
using AviaCompany.Application.Contracts.Passenger;
using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Passengers;
using AviaCompany.Domain.Models.Tickets;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с пассажирами
/// </summary>
public class PassengerService(IRepository<Passenger, int> repository, IRepository<Ticket, int> ticketRepository, IMapper mapper) : IPassengerService
{
    public async Task<PassengerDto> Create(PassengerCreateUpdateDto dto)
    {
        var passenger = mapper.Map<Passenger>(dto);
        var result = await repository.Create(passenger);
        return mapper.Map<PassengerDto>(result);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await repository.Delete(dtoId);
    }

    public async Task<PassengerDto?> Get(int dtoId)
    {
        var passenger = await repository.Read(dtoId);
        return passenger != null ? mapper.Map<PassengerDto>(passenger) : null;
    }

    public async Task<IList<PassengerDto>> GetAll()
    {
        var passengers = await repository.ReadAll();
        return mapper.Map<IList<PassengerDto>>(passengers);
    }

    public async Task<IList<TicketDto>> GetPassengerTicketsAsync(int passengerId)
    {
        var allTickets = await ticketRepository.ReadAll();

        var passengerTickets = allTickets
            .Where(ticket => ticket.PassengerId == passengerId)
            .OrderBy(ticket => ticket.FlightId)        
            .ThenBy(ticket => ticket.SeatNumber)       
            .ToList();

        return mapper.Map<IList<TicketDto>>(passengerTickets);
    }

    public async Task<PassengerDto> Update(PassengerCreateUpdateDto dto, int dtoId)
    {
        var passenger = mapper.Map<Passenger>(dto);
        passenger.Id = dtoId;
        var result = await repository.Update(passenger);
        return mapper.Map<PassengerDto>(result);
    }
}