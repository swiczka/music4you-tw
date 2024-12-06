using System.ComponentModel.DataAnnotations;

namespace music4you.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 10)]
        public int Value { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
