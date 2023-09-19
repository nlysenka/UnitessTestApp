using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnitessTestApp.Api.Core.DTO;
using UnitessTestApp.Api.Core.Entities;
using UnitessTestApp.Api.Core.Interfaces.Services;

namespace UnitessTestApp.Api.Controllers
{
    [Route("api/persons")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [Authorize]
        [HttpGet]
        public async Task<PersonPaginatedResponse> GetAllPersons([FromQuery] int pageSize, [FromQuery] int cursor)
        {
            var persons = await _personService.GetAllPersons(pageSize, cursor);
            return persons;
        }

        [Authorize]
        [HttpGet("detailed")]
        public async Task<List<Person>> GetAllPersonsWithDetails()
        {
            var persons = await _personService.GetAllPersonsWithDetails();
            return persons;
        }

        [Authorize]
        [HttpPost]
        public async Task CreatePerson([FromBody] string name = "default Name")
        {
            var newPerson = new Person(name);
            await _personService.CreatePerson(newPerson);
        }

        [Authorize]
        [HttpGet("{person_id:guid}")]
        public async Task<Person> GetPersonById([FromRoute(Name = "person_id")] Guid personId)
        {
            var person = await _personService.GetPerson(personId);
            return person;
        }

        [Authorize]
        [HttpPatch("{person_id:guid}")]
        public async Task UpdatePerson([FromRoute(Name = "person_id")] Guid personId, [FromBody] string name)
        {
            var updatedPerson = new Person
            {
                PersonId = personId,
                Name = name
            };

            await _personService.UpdatePerson(updatedPerson);
        }

        [Authorize]
        [HttpDelete("{person_id:guid}")]
        public async Task DeletePerson([FromRoute(Name = "person_id")] Guid personId)
        {
            await _personService.DeletePerson(personId);
        }
    }
}
