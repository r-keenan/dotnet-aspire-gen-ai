var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
        .AddPostgres("postgres")
        .WithPgAdmin()
        .WithDataVolume()
        .WithLifetime(ContainerLifetime.Persistent);

var basketDb = postgres.AddDatabase("basketDb");
var catalogDb = postgres.AddDatabase("catalogDb");

var basket = builder
        .AddProject<Projects.Basket>("basket")
        .WithReference(basketDb)
        .WaitFor(basketDb);
var catalog = builder
        .AddProject<Projects.Catalog>("catalog")
        .WithReference(catalogDb)
        .WaitFor(catalogDb);

builder.Build().Run();
