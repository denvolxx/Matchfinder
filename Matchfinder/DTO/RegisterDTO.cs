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

        [Required] public string KnownAs { get; set; } = string.Empty;
        [Required] public string Gender { get; set; } = string.Empty;
        [Required] public string DateOfBirth { get; set; } = string.Empty;
        [Required] public string City { get; set; } = string.Empty;
        [Required] public string Country { get; set; } = string.Empty;
    }
}
