using AutoMapper;
using AviaCompany.Application.Contracts.Passenger;
using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Domain;
using AviaCompany.Domain.Models.Passengers;

namespace AviaCompany.Application.Services;

/// <summary>
/// Сервис для работы с пассажирами
/// </summary>
public class PassengerService : IPassengerService
{
    private readonly IRepository<Passenger, int> _repository;
    private readonly IMapper _mapper;

    public PassengerService(IRepository<Passenger, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PassengerDto> Create(PassengerCreateUpdateDto dto)
    {
        var passenger = _mapper.Map<Passenger>(dto);
        var result = await _repository.Create(passenger);
        return _mapper.Map<PassengerDto>(result);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await _repository.Delete(dtoId);
    }

    public async Task<PassengerDto?> Get(int dtoId)
    {
        var passenger = await _repository.Read(dtoId);
        return passenger != null ? _mapper.Map<PassengerDto>(passenger) : null;
    }

    public async Task<IList<PassengerDto>> GetAll()
    {
        var passengers = await _repository.ReadAll();
        return _mapper.Map<IList<PassengerDto>>(passengers);
    }

    public async Task<IList<TicketDto>> GetPassengerTicketsAsync(int passengerId)
    {
        throw new NotImplementedException();
    }

    public async Task<PassengerDto> Update(PassengerCreateUpdateDto dto, int dtoId)
    {
        var passenger = _mapper.Map<Passenger>(dto);
        passenger.Id = dtoId;
        var result = await _repository.Update(passenger);
        return _mapper.Map<PassengerDto>(result);
    }
}