using System;
using System.Text;
using DailyRutine.Domain;
using DailyRutine.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DailyRutine.Application.Me.SetProfile
{
    public class SetProfileRequest : IRequest
    {
        public string Id { get; set; } = string.Empty;
        public string TimezoneId { get; set; } = string.Empty;
    }

    public class SetProfileRequestHandler : IRequestHandler<SetProfileRequest>
    {
        private readonly UserManager<User> _userManager;

        public SetProfileRequestHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SetProfileRequest request, CancellationToken cancellationToken)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(request.TimezoneId);
            if (tz is null) { throw new Error400Exception("Bad time zone id."); }
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null) { throw new Error400Exception("Bad identity."); }
            user.TimeZoneId = tz.Id;
            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                StringBuilder message = new StringBuilder();
                foreach (var e in result.Errors)
                {
                    message.Append(e.Description).Append('\n');
                }
                throw new Exception(message.ToString());
            }
            return Unit.Value;
        }
    }
}

