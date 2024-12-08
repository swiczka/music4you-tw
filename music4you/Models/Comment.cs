using System.ComponentModel.DataAnnotations;

namespace music4you.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
