using AutoMapper;
using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Grpc.Contracts;

namespace AviaCompany.WebApi.GrpcMappings;

/// <summary>
/// Маппинг между gRPC-контрактами и DTO для работы с билетами
/// </summary>
public class TicketGrpcMapper : Profile
{
    /// <summary>
    /// Конфигурация маппинга для преобразования gRPC-ответов в DTO
    /// </summary>
    public TicketGrpcMapper()
    {
        // Маппинг из TicketResponse (gRPC) в TicketCreateUpdateDto (Application)
        CreateMap<TicketResponse, TicketCreateUpdateDto>()
            .ForMember(dest => dest.FlightId,
                opt => opt.MapFrom(src => src.FlightId))
            .ForMember(dest => dest.PassengerId,
                opt => opt.MapFrom(src => src.PassengerId))
            .ForMember(dest => dest.SeatNumber,
                opt => opt.MapFrom(src => src.SeatNumber))
            .ForMember(dest => dest.HasHandLuggage,
                opt => opt.MapFrom(src => src.HasHandLuggage))
            .ForMember(dest => dest.LuggageWeight,
                opt => opt.MapFrom(src => (decimal?)src.BaggageWeight));
    }
}