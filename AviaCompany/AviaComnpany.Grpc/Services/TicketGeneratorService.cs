using AviaCompany.Grpc.Contracts;
using Bogus;
using Grpc.Core;

namespace AviaCompany.Grpc.Services;

/// <summary>
/// gRPC сервис-генератор билетов.
/// Генерирует случайные билеты на рейсы и отправляет их клиентам (API).
/// Обеспечивает двустороннюю потоковую связь с подтверждением сохранения.
/// </summary>
public class TicketGeneratorService(
    ILogger<TicketGeneratorService> logger,
    IConfiguration configuration)
    : TicketReceiver.TicketReceiverBase
{
    private readonly ILogger<TicketGeneratorService> _logger = logger;
    private readonly IConfiguration _configuration = configuration;
    private readonly Faker _faker = new();

    /// <summary>
    /// Потоковая генерация билетов.
    /// Клиент (API) подключается к сервису, генератор отправляет билеты в потоке,
    /// клиент обрабатывает их и возвращает статусы обработки.
    /// </summary>
    /// <param name="requestStream">Поток для получения callback-сообщений от клиента.</param>
    /// <param name="responseStream">Поток для отправки сгенерированных билетов клиенту.</param>
    /// <param name="context">Контекст gRPC вызова.</param>
    /// <returns>Задача, представляющая асинхронную операцию потоковой генерации.</returns>
    public override async Task StreamTickets(
        IAsyncStreamReader<TicketCallback> requestStream,
        IServerStreamWriter<TicketResponse> responseStream,
        ServerCallContext context)
    {
        _logger.LogInformation("Клиент подключился к генератору билетов");
        var delaySeconds = _configuration.GetValue<int>("Generator:DelaySeconds", 2);
        var generatedCount = 0;

        var receiveTask = Task.Run(async () =>
        {
            try
            {
                await foreach (var callback in requestStream.ReadAllAsync(context.CancellationToken))
                {
                    if (callback.Success)
                        _logger.LogDebug("Клиент подтвердил сохранение");
                    else
                        _logger.LogWarning("Клиент сообщил об ошибке: {Error}", callback.Error);
                }
            }
            catch (RpcException rpcEx) when (rpcEx.StatusCode == StatusCode.Cancelled)
            {
                _logger.LogInformation("Клиент отключился");
            }
        }, context.CancellationToken);

        var sendTask = Task.Run(async () =>
        {
            try
            {
                while (!context.CancellationToken.IsCancellationRequested)
                {
                    var ticket = GenerateRandomTicket();
                    generatedCount++;

                    _logger.LogInformation(
                        "Генерация билета #{Count}: Рейс={FlightId}, Пассажир={PassengerId}, Место={Seat}",
                        generatedCount, ticket.FlightId, ticket.PassengerId, ticket.SeatNumber);

                    await responseStream.WriteAsync(ticket, context.CancellationToken);
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds), context.CancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Генерация остановлена");
            }
        }, context.CancellationToken);

        await Task.WhenAll(receiveTask, sendTask);
        _logger.LogInformation("Сгенерировано билетов: {Count}", generatedCount);
    }

    /// <summary>
    /// Генерирует случайный билет с использованием библиотеки Bogus.
    /// </summary>
    /// <returns>Сгенерированный билет в формате gRPC-ответа.</returns>
    private TicketResponse GenerateRandomTicket()
    {
        return new TicketResponse
        {
            FlightId = _faker.Random.Int(1, 10),
            PassengerId = _faker.Random.Int(1, 10),
            SeatNumber = $"{_faker.Random.Int(1, 50)}{_faker.Random.Char('A', 'F')}",
            HasHandLuggage = _faker.Random.Bool(0.8f),
            BaggageWeight = _faker.Random.Bool(0.9f)
                ? Math.Round(_faker.Random.Double(5, 30), 2)
                : 0
        };
    }
}