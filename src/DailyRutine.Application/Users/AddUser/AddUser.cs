﻿using System;
using DailyRutine.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DailyRutine.Application.Users.AddUser
{
    public class AddUserRequest : IRequest<string>
    {
        public Shared.Users.AddUser.AddUserVm? Dto { get; set; }
    }

    public class AddUserRequestHandler : IRequestHandler<AddUserRequest, string>
    {
        private readonly UserManager<User> userManager;

        public AddUserRequestHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            if (request.Dto is null) { throw new ArgumentNullException("Dto","User dto is null."); }
            User user = MapToUser(request.Dto);
            var result = await userManager.CreateAsync(user);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw MapException(error);
                }
            }
            return user.Id;
        }

        private User MapToUser(Shared.Users.AddUser.AddUserVm source)
        {
            User user = new User();
            user.UserName = source.UserName;
            user.Email = source.Email;
            return user;
        }

        private Exception MapException(IdentityError err)
        {
            switch(err.Code)
            {
                case "DuplicateUserName": 
                case "InvalidUserName": { return new Shared.Exceptions.Error400Exception(err.Description); }
                default: { return new Exception($"Error code: {err.Code}\n Error message: {err.Description}\n"); }
            }
        }
    }
}

