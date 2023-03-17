using Dapper;
using UnitessTestApp.Api.Core.Entities;
using UnitessTestApp.Api.Core.Interfaces.Repositories;
using UnitessTestApp.Api.Data;

namespace UnitessTestApp.Api.Core.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DapperContext _context;

        public CarRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateCar(Car car)
        {
            string sqlQuery = "INSERT INTO Cars(PersonId, Model) " +
                              "OUTPUT INSERTED.CarId " +
                              "VALUES (@PersonId, @Model)";
            using var connection = _context.CreateConnection();
            var identity = await connection.QueryFirstOrDefaultAsync<Guid>(sqlQuery, new { car.PersonId, car.Model });

            return identity;
        }

        public async Task<Car> GetCar(Guid carId)
        {
            const string sqlQuery = "SELECT * FROM Cars WHERE CarId = @carId";
            using var connection = _context.CreateConnection();
            var car = await connection.QueryFirstOrDefaultAsync<Car>(sqlQuery, new { carId });
            return car;
        }

        public async Task<int> UpdateCar(Car car)
        {
            const string sqlQuery = "UPDATE Cars " +
                                    "SET PersonId = @personId, Model = @model " +
                                    "WHERE CarId = @carId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery, new { car.PersonId , car.Model, car.CarId });
            return affectedRows;
        }

        public async Task<int> DeleteCar(Guid carId)
        {
            const string sqlQuery = "DELETE FROM Cars WHERE CarId = @carId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery, new { carId });
            return affectedRows;
        }
    }
}
