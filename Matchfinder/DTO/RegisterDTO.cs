using System.ComponentModel.DataAnnotations;

namespace Matchfinder.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(16)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}
