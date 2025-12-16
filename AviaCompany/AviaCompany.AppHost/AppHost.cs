var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMySql("mysql-AviaCompany")
    .AddDatabase("AviaCompanyDb");

builder.AddProject<Projects.AviaCompany_WebApi>("aviacompany-api-host")
    .WithReference(db, "DefaultConnection")
    .WaitFor(db);

builder.AddProject<Projects.AviaCompany_Generator>("aviacompany-generator");

builder.Build().Run();