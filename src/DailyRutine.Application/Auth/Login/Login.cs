using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DailyRutine.Application.Options;
using DailyRutine.Domain;
using DailyRutine.Shared.Auth.Login;
using DailyRutine.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DailyRutine.Application.Auth.Login
{
    public class LoginRequest : IRequest<LoginResponseDto>
    {
        public LoginRequestDto? Dto { get; set; }
    }

    public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtOptions _jwtOptions;

        public LoginRequestHandler(UserManager<User> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<LoginResponseDto> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            if (request.Dto is null) { throw new ArgumentNullException("Dto", "User dto is null."); }
            User? user = await _userManager.FindByNameAsync(request.Dto.UserName);
            if (user is null) { throw new Error400Exception("Bad username or password."); }
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Dto.Password);
            if(passwordValid != true) { throw new Error400Exception("Bad username or password."); }

            string tokenString = await GenerateToken(user);

            return new LoginResponseDto() { UserId = user.Id, Email = user.Email, Token = tokenString };
        }

        private async Task<string> GenerateToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
                .Union(userClaims)
                .Union(roleClaims);


            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(_jwtOptions.Duration),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

