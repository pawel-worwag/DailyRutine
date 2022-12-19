using System;
using Microsoft.AspNetCore.Identity;

namespace DailyRutine.Domain
{
    public class User : IdentityUser
    {
        public string TimeZoneId { get; set; } = string.Empty;
    }
}