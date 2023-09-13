using Backend.Application.Config;
using Backend.Application.Contracts.Services.Caching;
using Backend.Infrastructure.Services.Caching;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ICachingService>(provider => new RedisCachingService(GlobalVariables.CacheConnectionString));
            
            return services;
        }
    }
}