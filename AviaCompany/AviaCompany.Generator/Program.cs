using AviaCompany.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();
app.MapGrpcService<TicketGeneratorService>();
app.MapGet("/", () => "Ticket Generator gRPC Server is running. Connect on port 5001");
app.Run();