using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Repositories;
using UserManagement.Repositories.Interfaces;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController(IUserRepository repo) : ControllerBase
    {
        [HttpGet("GetBySearch")]
        public async Task<IActionResult> Get([FromQuery] string search = "") => Ok(await repo.GetAllAsync(search));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repo.DeleteAsync(id);
            return Ok();
        }


        [HttpGet("Update")]
        public async Task<IActionResult> UpdateAsync([FromQuery] User User)
        {
            await repo.UpdateAsync(User);
            
            return Ok();
        }

    }
}
