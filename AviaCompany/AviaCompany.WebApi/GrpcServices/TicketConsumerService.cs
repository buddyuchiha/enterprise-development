using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using AviaCompany.Application.Contracts.Ticket;
using AviaCompany.Grpc.Contracts;

namespace AviaCompany.WebApi.GrpcServices;

/// <summary>
/// Фоновый сервис-потребитель билетов, работающий как gRPC клиент.
/// Подключается к удалённому генератору билетов, получает поток данных
/// и сохраняет их в базу данных с отправкой статусов обработки.
/// </summary>
public class TicketConsumerService(
    IServiceScopeFactory scopeFactory,
    ILogger<TicketConsumerService> logger,
    IConfiguration config,
    IMapper mapper) : BackgroundService
{
    private readonly string _generatorUrl = config["Generator:Address"]
                                            ?? "http://localhost:5001";

    /// <summary>
    /// Основной метод выполнения фонового сервиса.
    /// Устанавливает соединение с генератором, получает поток билетов
    /// и обрабатывает каждый билет с сохранением в БД.
    /// </summary>
    /// <param name="stoppingToken">Токен отмены для корректного завершения работы сервиса.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000, stoppingToken);

        logger.LogInformation("Подключение к генератору билетов по адресу: {Url}", _generatorUrl);

        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        using var channel = GrpcChannel.ForAddress(_generatorUrl, new GrpcChannelOptions
        {
            HttpHandler = httpHandler
        });

        var client = new TicketReceiver.TicketReceiverClient(channel);

        try
        {
            using var call = client.StreamTickets(cancellationToken: stoppingToken);

            await foreach (var ticketResponse in call.ResponseStream.ReadAllAsync(stoppingToken))
            {
                logger.LogInformation("Получен билет: Рейс={FlightId}, Пассажир={PassengerId}, Место={Seat}",
                    ticketResponse.FlightId, ticketResponse.PassengerId, ticketResponse.SeatNumber);

                var success = false;
                var error = string.Empty;

                try
                {
                    using var scope = scopeFactory.CreateScope();
                    var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();

                    var createDto = mapper.Map<TicketCreateUpdateDto>(ticketResponse);

                    await ticketService.Create(createDto);
                    success = true;
                    logger.LogDebug("Билет успешно сохранен в БД");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Ошибка при сохранении билета");
                    error = ex.Message;
                }

                await call.RequestStream.WriteAsync(new TicketCallback
                {
                    Success = success,
                    Error = error ?? ""
                }, stoppingToken);
            }
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            logger.LogInformation("Стрим был отменен.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Критическая ошибка в потребителе билетов");

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}