using AutoMapper;
using Matchfinder.DTO;
using Matchfinder.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Matchfinder.Controllers
{
    [Authorize]
    public class UsersController(IUserRepository userRepository) : BaseApiController
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
    }
}
