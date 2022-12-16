using System;
using System.ComponentModel.DataAnnotations;

namespace DailyRutine.Shared.Auth.Login
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

