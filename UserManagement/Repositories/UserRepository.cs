using Dapper;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Repositories.Interfaces;

namespace UserManagement.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _db;

        public UserRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetAllAsync(string search)
        {
            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<User>(
                "sp_Users_Search",
                new { Search = search },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task CreateAsync(User user)
        {
            using var conn = _db.CreateConnection();
            await conn.ExecuteAsync("sp_Users_Create", user,
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(User user)
        {
            using var conn = _db.CreateConnection();
            await conn.ExecuteAsync("sp_Users_Update", user,
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(int id)
        {
            using var conn = _db.CreateConnection();
            await conn.ExecuteAsync("sp_Users_SoftDelete",
                new { Id = id },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
