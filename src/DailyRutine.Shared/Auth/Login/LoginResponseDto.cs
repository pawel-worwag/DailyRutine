﻿using System;
namespace DailyRutine.Shared.Auth.Login
{
    public class LoginResponseDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}

