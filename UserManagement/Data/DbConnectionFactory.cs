using System.Data;
using Microsoft.Data.SqlClient;

namespace UserManagement.Data
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _config;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }
    }
}
