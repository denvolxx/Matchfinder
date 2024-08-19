using System.ComponentModel.DataAnnotations.Schema;

namespace Matchfinder.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        //Navigation Properties

        // Required foreign key property
        public int AppUserId { get; set; } 

        // Required reference navigation for one to many relashions
        public AppUser AppUser { get; set; } = null!; 
    }
}
