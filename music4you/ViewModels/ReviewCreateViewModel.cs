using music4you.Models;
using System.ComponentModel.DataAnnotations;

namespace music4you.ViewModels
{
    public class ReviewCreateViewModel
    {
        [Range(1, 10, ErrorMessage = "Podaj wartość z poprawnego zakresu")]
        [Required(ErrorMessage = "Podaj ocenę albumu")]
        [Display(Name = "Ocena w skali liczbowej (1-10)")]
        public int Value { get; set; }

        [Required(ErrorMessage = "Wprowadź nagłówek recenzji")]
        [Display(Name = "Nagłówek")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Wprowadź treść recenzji")]
        [Display(Name = "Treść recenzji")]
        public string Content { get; set; }
        public Album ?Album { get; set; }
        public int AlbumId { get; set; }
    }
}
