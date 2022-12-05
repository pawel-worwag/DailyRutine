using System;
using System.Reflection;
using DailyRutine.Application;
using Microsoft.EntityFrameworkCore;

namespace DailyRutine.Persistance
{
    public class DailyRutineDbContext : DbContext,IDailyRutineDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

