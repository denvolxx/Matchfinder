using System.ComponentModel.DataAnnotations;

namespace Matchfinder.DTO
{
    public class RegisterDTO
    {
        [MaxLength(16)]
        public required string Username { get; set; }


        public required string Password { get; set; }
    }
}
