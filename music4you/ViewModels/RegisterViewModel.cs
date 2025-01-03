using System.ComponentModel.DataAnnotations;

namespace music4you.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Adres e-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        public string Email { get; set; }

        [Display(Name = "Wyświetlana nazwa użytkownika")]
        [Required(ErrorMessage = "Unikalna nazwa użytkownika jest wymagana")]
        public string Username { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła muszą się zgadzać")]
        [Required(ErrorMessage = "Powtórzenie hasła jest wymagane")]
        public string ConfirmPassword { get; set; }
    }
}
