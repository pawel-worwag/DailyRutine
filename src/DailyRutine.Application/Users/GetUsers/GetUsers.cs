using System;
using DailyRutine.Shared.Users.GetUsers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyRutine.Application.Users.GetUsers
{
    public class GetUsersRequest : IRequest<GetUsersVm>
    {
        public int Take { get; set; } = 20;
        public int Skip { get; set; } = 0;
    }

    public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, GetUsersVm>
    {
        private readonly IDailyRutineDbContext _dbc;

        public GetUsersRequestHandler(IDailyRutineDbContext dbc)
        {
            _dbc = dbc;
        }

        public async Task<GetUsersVm> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            Shared.Users.GetUsers.GetUsersVm vm = new();
            vm.Count = await _dbc.Users.CountAsync();
            var users = await _dbc.Users.OrderBy(p => p.NormalizedUserName).Skip(request.Skip).Take(request.Take).ToListAsync();
            foreach(var u in users)
            {
                vm.Users.Add(new UserDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                });
            }
            return vm;
        }
    }
}

