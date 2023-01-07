using System;
using DailyRutine.Shared.Environment.GetAllowedTimeZones;
using MediatR;

namespace DailyRutine.Application.Environment.GetAllowedTimeZones
{
    public class GetAllowedTimeZonesRequest : IRequest<GetAllowedTimeZonesVm>
    {
    }
    public class GetAllowedTimeZonesRequestHandler : IRequestHandler<GetAllowedTimeZonesRequest, GetAllowedTimeZonesVm>
    {
        public Task<GetAllowedTimeZonesVm> Handle(GetAllowedTimeZonesRequest request, CancellationToken cancellationToken)
        {
            var dto = new GetAllowedTimeZonesVm();
            foreach (var tz in TimeZoneInfo.GetSystemTimeZones())
            {
                dto.TimeZones.Add(new TimeZoneDto()
                {
                    Id = tz.Id,
                    BaseUtcOffset = tz.BaseUtcOffset,
                    DisplayName = (tz.BaseUtcOffset.Ticks < 0) ? $"({tz.BaseUtcOffset.ToString(@"\-hh\:mm")}) {tz.Id}" :
                        (tz.BaseUtcOffset.Ticks == 0) ? $"({tz.BaseUtcOffset.ToString(@"hh\:mm")}) {tz.Id}" :
                        $"({tz.BaseUtcOffset.ToString(@"\+hh\:mm")}) {tz.Id}"
                }); ;
            }
            dto.TimeZones = dto.TimeZones.OrderBy(t => t.BaseUtcOffset).ThenBy(t => t.Id).ToList();
            return Task.FromResult(dto);
        }
    }
}

