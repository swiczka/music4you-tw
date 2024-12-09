using music4you.Models;
using System.ComponentModel.DataAnnotations;

namespace music4you.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string ?Title { get; set; }
        public string ?Content { get; set; }
        public int AlbumId { get; set; }
        public Album ?Album { get; set; }
        public string ?AppUserId { get; set; }
        public AppUser ?AppUser { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Zawartość komentarza jest wymagana")]
        [Display(Name = "Treść twojego komentarza")]
        public string Comment { get; set; }
        public List<Comment> ?Comments { get; set; }
    }
}
