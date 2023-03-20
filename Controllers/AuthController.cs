using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UnitessTestApp.Api.Core.Configuration;
using UnitessTestApp.Api.Core.DTO;
using UnitessTestApp.Api.Core.Exceptions;
using UnitessTestApp.Api.Core.Interfaces.Services;

namespace UnitessTestApp.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly TokenConfiguration _tokenConfiguration;
        public AuthController(IAuthService authService, TokenConfiguration tokenConfiguration)
        {
            _authService = authService;
            _tokenConfiguration = tokenConfiguration;
        }

        [HttpPost("token")]
        public string Token([FromBody] AuthData authData)
        {
            if (!_authService.Check(authData.Login, authData.Password))
            {
                throw new UnitessException(HttpStatusCode.Forbidden, "Authentication failed");
            }

            var jwt = new JwtSecurityToken(
                issuer: _tokenConfiguration.Issuer,
                audience: _tokenConfiguration.Audience,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(TokenConfiguration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
