using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyRutine.Api.Controllers
{
    [Route("/api/v1/me")]
    public class MeApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<Shared.Me.GetProfile.ProfileDto> GetProfile()
        {
            var uid = HttpContext.User.FindFirstValue("uid");
            return await _mediator.Send(new Application.Me.GetProfile.GetProfileRequest() { Id = uid});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetProfile(Shared.Me.SetProfile.ProfileDto profile)
        {
            var uid = HttpContext.User.FindFirstValue("uid");
            await _mediator.Send(new Application.Me.SetProfile.SetProfileRequest() { Id = uid, TimezoneId = profile.TimezoneId  });
            return Ok();
        }
    }
}

