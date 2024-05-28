var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.NazarAspire_ApiService>("apiservice");

builder.AddProject<Projects.NazarAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
