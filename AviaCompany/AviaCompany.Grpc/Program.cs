using AviaCompany.Grpc.Services;
using AviaCompany.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGrpcService<TicketGeneratorService>();

app.MapGet("/", () => "Ticket Generator gRPC Server is running. Connect on port 5001");

app.Run();