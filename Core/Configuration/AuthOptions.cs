using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UnitessTestApp.Api.Core.Configuration
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "MyAuthClient";
        private const string Key = "mysupersecret_secretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
    }
}