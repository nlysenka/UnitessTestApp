using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UnitessTestApp.Api.Core.Entities;
using UnitessTestApp.Api.Core.Exceptions;
using UnitessTestApp.Api.Core.Interfaces.Services;

namespace UnitessTestApp.Api.Controllers
{
    [Route("api/cars")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [Authorize]
        [HttpPost("car")]
        public async Task CreatePerson([FromBody] Car input)
        {
            if (ModelState.IsValid)
            {
                await _carService.CreateCar(input);
            }

            throw new UnitessException(HttpStatusCode.UnprocessableEntity, "Input model incorrect.");
        }

        [Authorize]
        [HttpGet("car/{car_id:guid}")]
        public async Task<Car> GetCarById([FromRoute(Name = "car_id")] Guid carId)
        {
            var car = await _carService.GetCar(carId);
            return car;
        }

        [Authorize]
        [HttpPatch("car/{car_id:guid}")]
        public async Task UpdatePerson([FromRoute(Name = "car_id")] Guid carId, [FromBody] Car input)
        {
            var updatedCar = new Car
            {
                CarId = carId,
                PersonId = input.PersonId,
                Model = input.Model
            };

            await _carService.UpdateCar(updatedCar);
            }

        [Authorize]
        [HttpDelete("car/{car_id:guid}")]
        public async Task DeleteCar([FromRoute(Name = "car_id")] Guid carId)
        {
            await _carService.DeleteCar(carId);
        }
    }
}
