namespace UnitessTestApp.Api.Core.Configuration
{
    public class AuthenticationConfiguration
    {
        public int SaltSize { get; set; }

        public int KeySize { get; set; }

        public int Iterations { get; set; }
    }
}
