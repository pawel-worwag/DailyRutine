using System;
namespace DailyRutine.Application
{
    public interface IDailyRutineDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

