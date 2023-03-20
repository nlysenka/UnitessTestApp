using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UnitessTestApp.Api.Core.Configuration
{
    public class TokenConfiguration
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public static string Key { get; set; }

        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
    }
}