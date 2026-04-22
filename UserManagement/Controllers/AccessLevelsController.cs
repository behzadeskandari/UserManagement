using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Repositories.Interfaces;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccessLevelsController(IAccessLevelRepository repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await repo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccessLevel al)
        {
            await repo.CreateAsync(al);
            return Ok(new { message = "Access Level created" });
        }
    }
}
