using Newtonsoft.Json;
using UnitessTestApp.Api.Core.Entities;

namespace UnitessTestApp.Api.Core.DTO
{
    public class PersonPaginatedResponse
    {
        [JsonProperty("persons")]
        public List<Person> Persons { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}
