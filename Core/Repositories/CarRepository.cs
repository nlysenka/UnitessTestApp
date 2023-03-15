using UnitessTestApp.Api.Core.Entities;
using UnitessTestApp.Api.Core.Interfaces.Repositories;

namespace UnitessTestApp.Api.Core.Repositories
{
    public class CarRepository : ICarRepository
    {
        public Task CreateCar(Car car)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCar(Car car)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCar(Guid idCar)
        {
            throw new NotImplementedException();
        }
    }
}
