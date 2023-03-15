using UnitessTestApp.Api.Core.Entities;

namespace UnitessTestApp.Api.Core.Interfaces.Repositories
{
    public interface ICarRepository
    {
        Task CreateCar(Car car);

        Task UpdateCar(Car car);

        Task DeleteCar(Guid idCar);
    }
}
