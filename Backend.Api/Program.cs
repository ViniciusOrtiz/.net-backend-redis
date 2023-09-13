using Microsoft.Extensions.Configuration;
using Backend.Persistence; 
using Microsoft.Extensions.Hosting;
using Backend.Infrastructure;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddInfrastructureServices();
        services.AddPersistenceServices();
    })
    .Build();

host.Run();