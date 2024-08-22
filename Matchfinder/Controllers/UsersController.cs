using AutoMapper;
using Matchfinder.DTO;
using Matchfinder.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Matchfinder.Controllers
{
    [Authorize]
    public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
    {
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsersAsync()
        {
            var users = await userRepository.GetMembersAsync();
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
            var user = await userRepository.GetMemberAsync(username);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO member)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (username == null)
                return BadRequest("No username found in token");

            var user = await userRepository.GetUserByNameAsync(username);
            if (user == null)
                return BadRequest("Could not find user");

            mapper.Map(member, user);

            if (await userRepository.SaveAllAsync())
                return NoContent();

            return BadRequest("User was NOT updated");
        }
    }
}
