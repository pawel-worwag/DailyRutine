using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DailyRutine.Api.Controllers
{
    [Route("/api/v1/users/")]
    public class UsersApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Shared.Users.GetUsers.GetUsersVm>> GetUsers(int Take=20, int Skip = 0)
        {
            return await _mediator.Send(new Application.Users.GetUsers.GetUsersRequest() { Take = Take, Skip = Skip });
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddUser(Shared.Users.AddUser.AddUserVm dto)
        {
            return await _mediator.Send(new Application.Users.AddUser.AddUserRequest() { Dto = dto });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, Shared.Users.UpdateUser.UpdateUserVm dto)
        {
            await _mediator.Send(new Application.Users.UpdateUser.UpdateUserRequest() { UserId = id, Dto = dto });
            return Ok();
        }
    }
}

