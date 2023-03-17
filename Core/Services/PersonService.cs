using System.Globalization;
using System.Net;
using UnitessTestApp.Api.Core.DTO;
using UnitessTestApp.Api.Core.Entities;
using UnitessTestApp.Api.Core.Exceptions;
using UnitessTestApp.Api.Core.Interfaces.Repositories;
using UnitessTestApp.Api.Core.Interfaces.Services;

namespace UnitessTestApp.Api.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Guid> CreatePerson(Person person)
        {
            try
            {
                var personId = await _personRepository.CreatePerson(person);
                return personId;
            }
            catch (Exception e)
            {
                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during creation of person. Error: {e.Message}.");
            }
        }

        public async Task<Person> GetPerson(Guid personId)
        {
            try
            {
                var person = await _personRepository.GetPerson(personId);

                if (person == null)
                {
                    throw new UnitessException(HttpStatusCode.NotFound, $"Person with {personId} is not found.");
                }

                return person;
            }
            catch (Exception e)
            {
                if (e is UnitessException)
                {
                    throw;
                }
                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during getting of person. Error: {e.Message}.");
            }
        }

        public async Task UpdatePerson(Person person)
        {
            try
            {
                var affectedRows = await _personRepository.UpdatePerson(person);
                if (affectedRows == 0)
                {
                    throw new UnitessException(HttpStatusCode.NotFound, $"Person with {person.PersonId} is not found.");
                }
            }
            catch (Exception e)
            {
                if (e is UnitessException)
                {
                    throw;
                }

                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during update of person. Error: {e.Message}.");
            }
        }

        public async Task DeletePerson(Guid personId)
        {
            try
            {
                var affectedRows = await _personRepository.DeletePerson(personId);
                if (affectedRows == 0)
                {
                    throw new UnitessException(HttpStatusCode.NotFound, $"Person with {personId} is not found.");
                }
            }
            catch (Exception e)
            {
                if (e is UnitessException)
                {
                    throw;
                }

                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during delete of person. Error: {e.Message}.");
            }
        }

        public async Task<PersonPaginatedResponse> GetAllPersons(int pageSize, int cursor)
        {
            if (cursor == 0)
            {
                cursor = 1;
            }

            var offset = 0;

            if (cursor > 1)
            {
                offset = (cursor-1) * pageSize;
            }

            if (pageSize < 1)
            {
                pageSize = 100;
            }

            try
            {
                var result = new PersonPaginatedResponse();

                var persons = await _personRepository.GetAllPersons(offset, pageSize);
                var personCount = await _personRepository.GetAllPersons(0, int.MaxValue);

                var metaData = CalculatePaginatedResponse(pageSize, cursor, personCount.Count);

                result.Persons = persons;
                result.Metadata = metaData;

                return result;
            }
            catch (Exception e)
            {
                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during get all persons. Error: {e.Message}.");
            }
        }

        public async Task<List<Person>> GetAllPersonsWithDetails()
        {
            try
            {
                var persons = await _personRepository.GetAllPersonsWithDetails();
                return persons;
            }
            catch (Exception e)
            {
                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during get all persons. Error: {e.Message}.");
            }
        }

        private Metadata CalculatePaginatedResponse(int pageSize, int cursor, int recordCount)
        {
            var response = new Metadata
            {
                TotalSize = recordCount
            };

            var allElementsOnPage = recordCount == pageSize && recordCount % pageSize == 0;

            var totalPages = allElementsOnPage ? 1 : recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1;
            response.TotalPages = totalPages;

            var nextCursor = allElementsOnPage
                ? null
                : Convert.ToInt32(cursor, CultureInfo.InvariantCulture) >= totalPages
                    ? null
                    : (Convert.ToInt32(cursor, CultureInfo.InvariantCulture) + 1)
                    .ToString(CultureInfo.InvariantCulture);

            response.NextCursor = nextCursor!;

            var previousCursor = (allElementsOnPage || cursor == 1)
                ? null
                : (Convert.ToInt32(cursor, CultureInfo.InvariantCulture) - 1)
                    .ToString(CultureInfo.InvariantCulture);

            response.PreviousCursor = previousCursor!;

            return response;
        }
    }
}
