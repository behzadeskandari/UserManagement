using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;

namespace UserManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController(DbConnectionFactory db) : ControllerBase
    {
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            using var conn = db.CreateConnection();
            // Using a multi-query or a specific SP for dashboard stats
            const string sql = @"
            SELECT COUNT(*) FROM Users WHERE IsDeleted = 0;
            SELECT COUNT(*) FROM AccessLevels;";

            using var multi = await conn.QueryMultipleAsync(sql);
            var userCount = await multi.ReadFirstAsync<int>();
            var roleCount = await multi.ReadFirstAsync<int>();

            return Ok(new
            {
                totalUsers = userCount,
                totalRoles = roleCount,
                serverTime = DateTime.Now
            });
        }
    }
}
