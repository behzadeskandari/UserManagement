using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Repositories.Interfaces;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "1")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccessLevelsController : ControllerBase
    {

        private readonly IAccessLevelRepository _repo;

        public AccessLevelsController(IAccessLevelRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _repo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccessLevel al)
        {
            await _repo.CreateAsync(al);
            return Ok(new { message = "Access Level created" });
        }
    }
}
