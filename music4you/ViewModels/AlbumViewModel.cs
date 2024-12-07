using music4you.Data;
using music4you.Models;
using System.ComponentModel.DataAnnotations;

namespace music4you.ViewModels
{
    public class AlbumViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
        public string ImageUrl { get; set; }
        public List<Rating> ?Ratings { get; set; }
        public Rating ?UserRating { get; set; }
    }
}
