using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Grpc.Contracts;
using Grpc.Core;
using Grpc.Net.Client;

namespace AviaCompany.WebApi.GrpcServices;

public class TicketGeneratorClientService : BackgroundService
{
    private readonly ILogger<TicketGeneratorClientService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public TicketGeneratorClientService(
        ILogger<TicketGeneratorClientService> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Сервис подключения к генератору запущен");
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ConnectToGeneratorAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка подключения к генератору. Повтор через 30 секунд");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }

    private async Task ConnectToGeneratorAsync(CancellationToken cancellationToken)
    {
        var generatorAddress = _configuration["Generator:Address"] ?? "http://localhost:5001";
        _logger.LogInformation("Подключение к генератору: {Address}", generatorAddress);

        using var channel = GrpcChannel.ForAddress(generatorAddress);
        var client = new TicketGenerator.TicketGeneratorClient(channel);
        using var call = client.StreamTickets(cancellationToken: cancellationToken);

        var receiveTask = Task.Run(async () =>
        {
            await foreach (var ticketResponse in call.ResponseStream.ReadAllAsync(cancellationToken))
            {
                try
                {
                    await ProcessTicketAsync(ticketResponse, cancellationToken);
                    await call.RequestStream.WriteAsync(new TicketCallback { Success = true }, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка обработки билета");
                    await call.RequestStream.WriteAsync(new TicketCallback { Success = false, Error = ex.Message }, cancellationToken);
                }
            }
        }, cancellationToken);

        await receiveTask;
    }

    private async Task ProcessTicketAsync(TicketResponse ticketResponse, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();

        var ticketDto = new TicketCreateUpdateDto(
            FlightId: ticketResponse.FlightId,
            PassengerId: ticketResponse.PassengerId,
            SeatNumber: ticketResponse.SeatNumber,
            HasHandLuggage: ticketResponse.HasHandLuggage,
            LuggageWeight: (decimal?)ticketResponse.BaggageWeight  
        );

        var createdTicket = await ticketService.Create(ticketDto);
        _logger.LogInformation("Билет сохранен: ID={TicketId}, Рейс={FlightId}, Пассажир={PassengerId}",
            createdTicket.Id, createdTicket.FlightId, createdTicket.PassengerId);
    }
}