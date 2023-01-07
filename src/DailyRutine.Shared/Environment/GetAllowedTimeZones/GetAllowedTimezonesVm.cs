using System;
namespace DailyRutine.Shared.Environment.GetAllowedTimeZones
{
    public class GetAllowedTimeZonesVm
    {
        public ICollection<TimeZoneDto> TimeZones { get; set; } = new List<TimeZoneDto>();
    }
}

