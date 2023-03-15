namespace UnitessTestApp.Api.Core.Entities
{
    public class Car
    {
        public Guid CarId { get; set; }
        public Guid? PersonId { get; set; }
        public string Model { get; set; }
    }
}
