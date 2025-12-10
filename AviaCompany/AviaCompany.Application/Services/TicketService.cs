using AutoMapper;
using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Tickets;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с билетами
/// </summary>
public class TicketService : ITicketService
{
    private readonly IRepository<Ticket, int> _repository;
    private readonly IMapper _mapper;

    public TicketService(IRepository<Ticket, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TicketDto> Create(TicketCreateUpdateDto dto)
    {
        var ticket = _mapper.Map<Ticket>(dto);
        var result = await _repository.Create(ticket);
        return _mapper.Map<TicketDto>(result);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await _repository.Delete(dtoId);
    }

    public async Task<TicketDto?> Get(int dtoId)
    {
        var ticket = await _repository.Read(dtoId);
        return ticket != null ? _mapper.Map<TicketDto>(ticket) : null;
    }

    public async Task<IList<TicketDto>> GetAll()
    {
        var tickets = await _repository.ReadAll();
        return _mapper.Map<IList<TicketDto>>(tickets);
    }

    public async Task<IList<TicketDto>> GetTicketsByFlightAsync(int flightId)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<TicketDto>> GetTicketsByPassengerAsync(int passengerId)
    {
        throw new NotImplementedException();
    }

    public async Task<TicketDto> Update(TicketCreateUpdateDto dto, int dtoId)
    {
        var ticket = _mapper.Map<Ticket>(dto);
        ticket.Id = dtoId;
        var result = await _repository.Update(ticket);
        return _mapper.Map<TicketDto>(result);
    }
}