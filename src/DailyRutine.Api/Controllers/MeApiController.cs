using System;
using Microsoft.AspNetCore.Mvc;

namespace DailyRutine.Api.Controllers
{
    [Route("/api/v1/me")]
    public class MeApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProfile()
        {
            return Ok($"Authenticated: {HttpContext.User.Identity.IsAuthenticated}; Name: {HttpContext.User.Identity.Name}");
        }
    }
}

