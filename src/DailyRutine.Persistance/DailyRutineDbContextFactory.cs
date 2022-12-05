using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DailyRutine.Persistance
{
    public class DailyRutineDbContextFactory : IDesignTimeDbContextFactory<DailyRutineDbContext>
    {
        private const string ConnectionStringName = "DailyRutineDb";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public DailyRutineDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var environmentName = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);
            var optionsBuilder = new DbContextOptionsBuilder<DailyRutineDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new DailyRutineDbContext(optionsBuilder.Options);
        }
    }
}

