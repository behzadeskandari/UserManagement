using Dapper;
using Microsoft.AspNetCore.Identity;
using UserManagement.Data;
using UserManagement.Helpers;
using UserManagement.Models;
using UserManagement.Repositories.Interfaces;

namespace UserManagement.Services
{
    public class AuthService
    {
        private readonly DbConnectionFactory _db;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthService(DbConnectionFactory db, IConfiguration config, IUserRepository userRepository)
        {
            _db = db;
            _config = config;
            _userRepository = userRepository;
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


        public async Task<String> Register(string UserName,string password)
        {
            using var conn = _db.CreateConnection();

            await conn.QueryFirstOrDefaultAsync<User>(
            "sp_Users_Create",
                new { FullName = UserName , Username = UserName , Email = UserName , PasswordHash = PasswordHelper.Hash(password),
                    AccessLevelId = 1,
                },
                commandType: System.Data.CommandType.StoredProcedure);

           
            //

            return  password;
        }
    }
}
