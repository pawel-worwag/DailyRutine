using System;
namespace DailyRutine.Shared.Environment.GetAllowedTimeZones
{
    public class TimeZoneDto
    {
        public string Id { get; set; } = string.Empty;
        public TimeSpan BaseUtcOffset { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }
}

