using AutoMapper;
using Matchfinder.DTO;
using Matchfinder.Entities;
using Matchfinder.Extensions;
using Matchfinder.Helpers;
using Matchfinder.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Matchfinder.Controllers
{
    [Authorize]
    public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseApiController
    {
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsersAsync([FromQuery] UserParams userParams)
        {
            var users = await userRepository.GetMembersAsync(userParams);
            if (users == null)
                return NotFound();

            Response.AddPaginationHeader(users);
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUserAsync(string username)
        {
            var user = await userRepository.GetMemberAsync(username);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO member)
        {
            var user = await userRepository.GetUserByNameAsync(User.GetUsername());
            if (user == null)
                return NotFound("Could not find user");

            mapper.Map(member, user);

            bool isSaved = await userRepository.SaveAllAsync();
            if (isSaved)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("User was NOT updated");
            }
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
        {
            var user = await userRepository.GetUserByNameAsync(User.GetUsername());
            if (user == null)
                return NotFound("Could not find user");

            var result = await photoService.AddPhotoAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            user.Photos.Add(photo);

            bool isSaved = await userRepository.SaveAllAsync();
            if (isSaved)
            {
                return CreatedAtAction(nameof(GetUserAsync), new { username = user.UserName }, mapper.Map<PhotoDTO>(photo));
            }
            else
            {
                return BadRequest("Problem adding photo");
            }
        }

        [HttpPut("set-main-photo/{photoId:int}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await userRepository.GetUserByNameAsync(User.GetUsername());
            if (user == null) return NotFound("Could not find user");

            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null) return NotFound("Could not find photo");
            if (photo.IsMain) return BadRequest("This is already a main photo");

            var currentMain = user.Photos.FirstOrDefault(p => p.IsMain);

            if (currentMain != null)
                currentMain.IsMain = false;

            photo.IsMain = true;

            bool isSaved = await userRepository.SaveAllAsync();
            if (isSaved)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Main photo was not changed");
            }
        }

        [HttpDelete("delete-photo/{photoId:int}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await userRepository.GetUserByNameAsync(User.GetUsername());
            if (user == null) return NotFound("User does not exist or were already deleted");

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null) return NotFound("Could not find photo ");
            if (photo.IsMain) return BadRequest("Can not delete main photo");

            //Delete photo from cloud
            if (photo.PublicId != null)
            {
                var result = await photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            bool isSaved = await userRepository.SaveAllAsync();
            if (isSaved)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Problem deleting photo");
            }
        }
    }
}
