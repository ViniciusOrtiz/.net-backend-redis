using Backend.Application.Config;
using Backend.Application.Contracts;
using Backend.Application.Contracts.Repositories;
using Backend.Persistence.DatabaseContexts;
using Backend.Persistence.Repositories;
using Backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(option =>
            {
                option.UseSqlServer(GlobalVariables.ConnectionString);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMedalhaRepository, MedalhaRepository>();

            return services;
        }
    }
}