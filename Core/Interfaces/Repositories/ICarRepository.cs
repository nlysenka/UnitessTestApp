using UnitessTestApp.Api.Core.Entities;

namespace UnitessTestApp.Api.Core.Interfaces.Repositories
{
    public interface ICarRepository
    {
        Task<Guid> CreateCar(Car car);

        Task<Car> GetCar(Guid carId);

        Task<int> UpdateCar(Car car);

        Task<int> DeleteCar(Guid carId);
    }
}
