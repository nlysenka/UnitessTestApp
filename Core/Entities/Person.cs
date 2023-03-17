namespace UnitessTestApp.Api.Core.Entities
{
    public class Person
    {
        public Person()
        {
        }

        public Person(string name)
        {
            Name = name;
        }

        public Guid PersonId { get; set; }
        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
