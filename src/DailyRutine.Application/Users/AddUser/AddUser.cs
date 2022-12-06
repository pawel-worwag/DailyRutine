using System;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DailyRutine.Application.Users.AddUser
{
    public class AddUserRequest : IRequest<string>
    {
        public Shared.Users.AddUser.AddUserVm? Dto { get; set; }
    }

    public class AddUserRequestHandler : IRequestHandler<AddUserRequest, string>
    {
        private readonly UserManager<IdentityUser> userManager;

        public AddUserRequestHandler(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            if (request.Dto is null) { throw new ArgumentNullException("User dto in null"); }
            IdentityUser user = MapToUser(request.Dto);
            var result = await userManager.CreateAsync(user);
            if(result.Succeeded)
            {
                return user.Id;
            }
            else
            {
                string message = "";
                foreach(var error in result.Errors)
                {
                    message += error.Description;
                }
                throw new Exception(message);
            }
        }

        private IdentityUser MapToUser(Shared.Users.AddUser.AddUserVm source)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = source.UserName;
            user.Email = source.Email;
            return user;
        }
    }
}

