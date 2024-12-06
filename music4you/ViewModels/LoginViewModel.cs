using System.ComponentModel.DataAnnotations;

namespace music4you.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Adres e-mail")]
        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
