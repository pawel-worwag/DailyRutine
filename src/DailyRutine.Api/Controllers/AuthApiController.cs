using System;
using DailyRutine.Shared.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyRutine.Api.Controllers
{
    [Route("/api/v1/auth/")]
    public class AuthApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto userDto)
        {
            return await _mediator.Send(new Application.Auth.Login.LoginRequest() { Dto = userDto });
        }
    }
}

