using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DailyRutine.Application
{
    public interface IDailyRutineDbContext
    {
        public DbSet<IdentityUser> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

