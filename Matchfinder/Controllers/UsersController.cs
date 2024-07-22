using Matchfinder.Data;
using Matchfinder.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Matchfinder.Controllers
{
    public class UsersController(DataContext context) : BaseApiController
    {

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsersAsync()
        {
            var users = await context.Users.ToListAsync();
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
