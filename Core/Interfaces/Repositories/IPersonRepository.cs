using UnitessTestApp.Api.Core.Entities;

namespace UnitessTestApp.Api.Core.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<Guid> CreatePerson(Person person);

        Task<Person> GetPerson(Guid personId);

        Task<int> UpdatePerson(Person person);

        Task<int> DeletePerson(Guid personId);

        Task<List<Person>> GetAllPersons(int offset, int fetch);

        Task<List<Person>> GetAllPersonsWithDetails();
    }
}
