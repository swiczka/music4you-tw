using music4you.Data;
using System.ComponentModel.DataAnnotations;

namespace music4you.ViewModels
{
    public class AlbumCreateViewModel
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa albumu jest wymagana")]
        public string Name { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Autor albumu jest wymagany")]
        public string Author { get; set; }

        [Display(Name = "Rok wydania")]
        [Required(ErrorMessage = "Rok wydania jest wymagany")]
        [Range(1900, 2024, ErrorMessage = "Podaj poprawny rok wydania" )]
        public int Year { get; set; }

        [Display(Name = "Gatunek")]
        [Required(ErrorMessage = "Gatunek jest wymagany")]
        public Genre Genre { get; set; }

        [Display(Name = "Link do okładki")]
        [Required(ErrorMessage = "Link do okładki jest wymagany")]
        public string ImageUrl { get; set; }
    }
}
