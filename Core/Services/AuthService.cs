using System.Net;
using System.Security.Cryptography;
using UnitessTestApp.Api.Core.Configuration;
using UnitessTestApp.Api.Core.Exceptions;
using UnitessTestApp.Api.Core.Interfaces.Services;

namespace UnitessTestApp.Api.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly Dictionary<string, string?> _users = new()
        {
            {"admin", "8.+tYHZSVSRG8X0tbRf6OimA==.WspcWB1axeWugM2GihpDXdp7UE7+2BS1ElT6S4oJJo8="} //password
        };

        private readonly AuthenticationConfiguration _authenticationConfig;

        public AuthService(AuthenticationConfiguration authenticationConfig)
        {
            _authenticationConfig = authenticationConfig;
        }

        public string Hash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(password, _authenticationConfig.SaltSize, _authenticationConfig.Iterations, HashAlgorithmName.SHA512);

            var key = Convert.ToBase64String(algorithm.GetBytes(_authenticationConfig.KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{_authenticationConfig.Iterations}.{salt}.{key}";
        }

        public bool Check(string login, string password)
        {
            if(!_users.Any(p => p.Key == login))
            {
                throw new UnitessException(HttpStatusCode.NotFound, $@"Login {login} not found");
            }

            _users.TryGetValue(login, out string hash);

            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new UnitessException(HttpStatusCode.Forbidden, "Unexpected hash format.");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA512);

            var keyToCheck = algorithm.GetBytes(_authenticationConfig.KeySize);
            var verified = keyToCheck.SequenceEqual(key);

            return verified;
        }
    }
}
