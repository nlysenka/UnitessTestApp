using System;
using System.Net;
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
                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during creation of person. Error: {e.Message}.");
            }
        }

        public async Task UpdatePerson(Person person)
        {
            try
            {
                var updatedPerson = new Person
                {
                    PersonId = person.PersonId,
                    Name = person.Name
                };

                var affectedRows = await _personRepository.UpdatePerson(updatedPerson);
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

        public async Task<List<Person>> GetAllPersons()
        {
            try
            {
                var persons = await _personRepository.GetAllPersons();
                return persons;
            }
            catch (Exception e)
            {
                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during get all persons. Error: {e.Message}.");
            }
        }
    }
}
