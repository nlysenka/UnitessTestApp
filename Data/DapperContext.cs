using Microsoft.Data.SqlClient;
using System.Data;
using UnitessTestApp.Api.Core.Configuration;

namespace UnitessTestApp.Api.Data
{
    public class DapperContext
    {
        private readonly UnitessConfiguration _unitessConfiguration;

        public DapperContext(UnitessConfiguration unitessConfiguration)
        {
            _unitessConfiguration = unitessConfiguration;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_unitessConfiguration.ConnectionString);
    }
}
