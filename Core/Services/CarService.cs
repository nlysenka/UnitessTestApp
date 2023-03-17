using System.Net;
using UnitessTestApp.Api.Core.Entities;
using UnitessTestApp.Api.Core.Exceptions;
using UnitessTestApp.Api.Core.Interfaces.Repositories;
using UnitessTestApp.Api.Core.Interfaces.Services;

namespace UnitessTestApp.Api.Core.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Guid> CreateCar(Car car)
        {
            try
            {
                var carr = new Car
                {
                    PersonId = car.PersonId,
                    Model = car.Model
                };

                var carId = await _carRepository.CreateCar(carr);
                return carId;
            }
            catch (Exception e)
            {
                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during creation of car. Error: {e.Message}.");
            }
        }

        public async Task<Car> GetCar(Guid carId)
        {
            try
            {
                var car = await _carRepository.GetCar(carId);

                if (car == null)
                {
                    throw new UnitessException(HttpStatusCode.NotFound, $"Person with {carId} is not found.");
                }

                return car;
            }
            catch (Exception e)
            {
                if (e is UnitessException)
                {
                    throw;
                }
                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during getting of car. Error: {e.Message}.");
            }
        }

        public async Task UpdateCar(Car car)
        {
            try
            {
                var affectedRows = await _carRepository.UpdateCar(car);
                if (affectedRows == 0)
                {
                    throw new UnitessException(HttpStatusCode.NotFound, $"Car with {car.CarId} is not found.");
                }
            }
            catch (Exception e)
            {
                if (e is UnitessException)
                {
                    throw;
                }

                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during update of car. Error: {e.Message}.");
            }
        }

        public async Task DeleteCar(Guid carId)
        {
            try
            {
                var affectedRows = await _carRepository.DeleteCar(carId);
                if (affectedRows == 0)
                {
                    throw new UnitessException(HttpStatusCode.NotFound, $"Car with carId {carId} is not found.");
                }
            }
            catch (Exception e)
            {
                if (e is UnitessException)
                {
                    throw;
                }

                throw new UnitessException(HttpStatusCode.InternalServerError, $"Error during delete of car. Error: {e.Message}.");
            }
        }
    }
}
