using Microsoft.AspNetCore.Mvc;
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

        //TODO add pagination
        [HttpGet("all")]
        public async Task<List<Person>> GetAllPersons()
        {
            var persons = await _personService.GetAllPersons();
            return persons;
        }

        [HttpPost("person")]
        public async Task CreatePerson([FromBody] string name ="default Name")
        {
            var newPerson = new Person(name);
            await _personService.CreatePerson(newPerson);
        }

        [HttpGet("person/{person_id:guid}")]
        public async Task<Person> GetPersonById([FromRoute(Name = "person_id")] Guid personId)
        {
            var person = await _personService.GetPerson(personId);
            return person;
        }

        [HttpPatch("person/{person_id:guid}")]
        public async Task UpdatePerson([FromRoute(Name = "person_id")] Guid personId,[FromBody] string name)
        {
            var updatedPerson = new Person
            {
                PersonId = personId,
                Name = name
            };

            await _personService.UpdatePerson(updatedPerson);
        }

        [HttpDelete("person/{person_id:guid}")]
        public async Task DeletePerson([FromRoute(Name = "person_id")] Guid personId)
        {
          await _personService.DeletePerson(personId);
        }
    }
}
