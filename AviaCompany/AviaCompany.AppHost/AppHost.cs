var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMySql("mysql-AviaCompany")
    .AddDatabase("AviaCompanyDb");

// 1. —начала добавл€ем генератор
var generator = builder.AddProject<Projects.AviaCompany_Grpc>("aviacompany-grpc");

// 2. ѕотом API, который ссылаетс€ на генератор
var api = builder.AddProject<Projects.AviaCompany_WebApi>("aviacompany-api")
    .WithReference(db, "DefaultConnection")
    .WithReference(generator)  // ссылка на генератор
    .WaitFor(db);

builder.Build().Run();