using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Repositories;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController(UserRepository repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string search = "") => Ok(await repo.GetAllAsync(search));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repo.DeleteAsync(id);
            return Ok();
        }
    }
}
