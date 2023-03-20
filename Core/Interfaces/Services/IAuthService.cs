namespace UnitessTestApp.Api.Core.Interfaces.Services
{
    public interface IAuthService
    {
        string Hash(string password);

        bool Check(string login, string password);
    }
}
