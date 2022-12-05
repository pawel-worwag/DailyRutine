using System;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DailyRutine.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

