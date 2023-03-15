using Dapper;
using UnitessTestApp.Api.Core.Entities;
using UnitessTestApp.Api.Core.Interfaces.Repositories;
using UnitessTestApp.Api.Data;

namespace UnitessTestApp.Api.Core.Repositories
{
    public class PersonRepository: IPersonRepository
    {
        private readonly DapperContext _context;

        public PersonRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreatePerson(Person person)
        {
            string sqlQuery = "INSERT INTO Persons (Name) " +
                              "OUTPUT INSERTED.PersonId " +
                              "VALUES (@Name)";
            using var connection = _context.CreateConnection();
            var identity = await connection.QueryFirstOrDefaultAsync<Guid>(sqlQuery, new {person.Name});

            return identity;
        }

        public async Task<Person> GetPerson(Guid personId)
        {
            const string sqlQuery = "SELECT * FROM Persons WHERE PersonId = @personId";
            using var connection = _context.CreateConnection();
            var person = await connection.QueryFirstOrDefaultAsync<Person>(sqlQuery, new { personId });
            return person;
        }

        public async Task<int> UpdatePerson(Person person)
        {
            const string sqlQuery = "UPDATE Persons " +
                                    "SET Name = @name " +
                                    "WHERE PersonId = @personId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery, new { person.PersonId, person.Name });
            return affectedRows;
        }

        public async Task<int> DeletePerson(Guid personId)
        {
            const string sqlQuery = "DELETE FROM Persons WHERE PersonId = @personId";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery, new {personId });
            return affectedRows;
        }

        public async Task<List<Person>> GetAllPersons()
        {
            const string sqlQuery = "SELECT * FROM Persons";
            using var connection = _context.CreateConnection();
            var users = await connection.QueryAsync<Person>(sqlQuery);
            return users.ToList();
        }
    }
}
