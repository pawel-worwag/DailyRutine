using System;
using MediatR;
using DailyRutine.Shared.Users.UpdateUser;
using Microsoft.AspNetCore.Identity;
using DailyRutine.Shared.Exceptions;

namespace DailyRutine.Application.Users.UpdateUser
{
    public class UpdateUserRequest : IRequest
    {
        public string UserId { get; set; } = string.Empty;
        public UpdateUserVm? Dto { get; set; } = null;
    }
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest>
    {

        private readonly UserManager<IdentityUser> _userManager;

        public UpdateUserRequestHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.UserId)) { throw new ArgumentNullException("UserId", "User id is null or empty."); }
            if (request.Dto is null) { throw new ArgumentNullException("Dto","User dto is null."); }
            IdentityUser? user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null) { throw new Error404Exception("User not found."); }
            user.UserName = request.Dto.UserName;
            user.Email = request.Dto.Email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw MapException(error);
                }
            }
            return Unit.Value;
        }

        private Exception MapException(IdentityError err)
        {
            switch (err.Code)
            {
                case "DuplicateUserName":
                case "InvalidUserName": { return new Shared.Exceptions.Error400Exception(err.Description); }
                default: { return new Exception($"Error code: {err.Code}\n Error message: {err.Description}\n"); }
            }
        }
    }
}

