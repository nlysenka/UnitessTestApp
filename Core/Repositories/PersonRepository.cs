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

        public async Task<List<Person>> GetAllPersons(int offsetParam, int fetchParam)
        {
            const string sqlQuery = "SELECT * " +
                                    "FROM Persons " +
                                    "ORDER BY PersonId " +
                                    "OFFSET @offsetParam rows " +
                                    "FETCH NEXT @fetchParam ROWS ONLY";
            using var connection = _context.CreateConnection();
            var users = await connection.QueryAsync<Person>(sqlQuery, new { offsetParam, fetchParam });
            return users.ToList();
        }

        public async Task<List<Person>> GetAllPersonsWithDetails()
        {
            const string sqlQuery = "SELECT p.PersonId, p.Name, c.CarId, c.PersonId, c.Model " +
                                    "FROM Persons p " +
                                    "LEFT JOIN Cars c on p.PersonId = c.PersonId " +
                                    "ORDER BY p.PersonId " +
                                    "OFFSET 1 ROWS";
            using var connection = _context.CreateConnection();

            var lookup = new Dictionary<Guid, Person>();
            await connection.QueryAsync<Person, Car, Person>(sqlQuery, (s, a) =>
                {
                    if (!lookup.TryGetValue(s.PersonId, out var person))
                    {
                        lookup.Add(s.PersonId, person = s);
                    }
                    person.Cars.Add(a);
                    return person;
                }, splitOn: "CarId"
            );

            var resultList = lookup.Values.ToList();
            
            return resultList;
        }
    }
}
