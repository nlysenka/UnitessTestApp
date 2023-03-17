using UnitessTestApp.Api.Core.DTO;
using UnitessTestApp.Api.Core.Entities;

namespace UnitessTestApp.Api.Core.Interfaces.Services
{
    public interface IPersonService
    {
        Task<Guid> CreatePerson(Person person);

        Task<Person> GetPerson(Guid personId);

        Task UpdatePerson(Person person);

        Task DeletePerson(Guid personId);

        Task<PersonPaginatedResponse> GetAllPersons(int pageSize, int cursor);

        Task<List<Person>> GetAllPersonsWithDetails();
    }
}
