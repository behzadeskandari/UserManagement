using Dapper;
using System.Data;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Repositories.Interfaces;

namespace UserManagement.Repositories
{
    public class AccessLevelRepository(DbConnectionFactory db) : IAccessLevelRepository
    {
        public async Task<IEnumerable<AccessLevel>> GetAllAsync()
        {
            using var conn = db.CreateConnection();
            return await conn.QueryAsync<AccessLevel>(
                "sp_AccessLevels_GetAll",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<AccessLevel?> GetByIdAsync(int id)
        {
            using var conn = db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<AccessLevel>(
                "sp_AccessLevels_GetById",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(AccessLevel accessLevel)
        {
            using var conn = db.CreateConnection();
            await conn.ExecuteAsync("sp_AccessLevels_Create",
                new { accessLevel.Name, accessLevel.Description },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(AccessLevel accessLevel)
        {
            using var conn = db.CreateConnection();
            await conn.ExecuteAsync("sp_AccessLevels_Update",
                new { accessLevel.Id, accessLevel.Name, accessLevel.Description },
                commandType: CommandType.StoredProcedure);
        }
    }
}
