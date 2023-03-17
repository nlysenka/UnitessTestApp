using UnitessTestApp.Api.Core.Entities;

namespace UnitessTestApp.Api.Core.Interfaces.Services
{
    public interface ICarService
    {
        Task<Guid> CreateCar(Car car);

        Task<Car> GetCar(Guid carId);

        Task UpdateCar(Car car);

        Task DeleteCar(Guid carId);
    }
}
