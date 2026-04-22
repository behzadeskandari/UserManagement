using Dapper;
using UserManagement.Data;
using UserManagement.Helpers;
using UserManagement.Models;

namespace UserManagement.Services
{
    public class AuthService
    {
        private readonly DbConnectionFactory _db;
        private readonly IConfiguration _config;

        public AuthService(DbConnectionFactory db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<string?> Login(string username, string password)
        {
            using var conn = _db.CreateConnection();

            var user = await conn.QueryFirstOrDefaultAsync<User>(
                "sp_Users_GetByUsername",
                new { Username = username },
                commandType: System.Data.CommandType.StoredProcedure);

            if (user == null) return null;

            if (!PasswordHelper.Verify(password, user.PasswordHash))
                return null;

            return JwtHelper.Generate(user, _config["Jwt:Key"]);
        }
    }
}
