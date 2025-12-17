var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMySql("mysql-AviaCompany")
    .AddDatabase("AviaCompanyDb");

var generator = builder.AddProject<Projects.AviaCompany_Grpc>("aviacompany-grpc");

var api = builder.AddProject<Projects.AviaCompany_WebApi>("aviacompany-api")
    .WithReference(db, "DefaultConnection")
    .WithReference(generator)  
    .WaitFor(db);

builder.Build().Run();