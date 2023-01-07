using System;
using DailyRutine.Domain;
using DailyRutine.Shared.Me.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DailyRutine.Application.Me.GetProfile
{
    public class GetProfileRequest : IRequest<ProfileDto>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class GetProfileRequestHandler : IRequestHandler<GetProfileRequest, ProfileDto>
    {
        private readonly UserManager<User> _userManager;

        public GetProfileRequestHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ProfileDto> Handle(GetProfileRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            var dto = new ProfileDto()
            {
                TimezoneId = user.TimeZoneId
            };
            return dto;
        }
    }
}

