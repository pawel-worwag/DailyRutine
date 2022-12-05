using System;
using DailyRutine.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyRutine.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DailyRutineDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DailyRutineDb")));
            services.AddScoped<IDailyRutineDbContext, DailyRutineDbContext>();
            return services;
        }
    }
}

