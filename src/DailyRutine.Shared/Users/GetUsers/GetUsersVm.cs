using System;
namespace DailyRutine.Shared.Users.GetUsers
{
    public class GetUsersVm
    {
        public ICollection<UserDto> Users { get; set; } = new List<UserDto>();
        public int Count { get; set; } = 0;
    }
}

