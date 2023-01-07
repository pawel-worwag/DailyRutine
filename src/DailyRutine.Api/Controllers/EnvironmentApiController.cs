using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyRutine.Api.Controllers
{
    public class EnvironmentApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnvironmentApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/v1/timezones")]
        public async Task<Shared.Environment.GetAllowedTimeZones.GetAllowedTimeZonesVm> GetProfile()
        {
            return await _mediator.Send(new Application.Environment.GetAllowedTimeZones.GetAllowedTimeZonesRequest());
        }
    }
}

