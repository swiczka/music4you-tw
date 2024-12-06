using music4you.Data;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace music4you.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
        public string ImageUrl { get; set; }
    }
}
