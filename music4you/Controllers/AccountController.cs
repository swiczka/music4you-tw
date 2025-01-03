using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using music4you.Data;
using music4you.Interface;
using music4you.Models;
using music4you.ViewModels;
using System.Text.RegularExpressions;

namespace music4you.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordValidator<AppUser> _passwordValidator;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IReviewRepository _reviewRepository;

        public AccountController(UserManager<AppUser> userManager, IPasswordValidator<AppUser> passwordValidator, SignInManager<AppUser> signInManager, IReviewRepository reviewRepository)
        {
            _userManager = userManager;
            _passwordValidator = passwordValidator;
            _signInManager = signInManager;
            _reviewRepository = reviewRepository;
        }

        public async Task<IActionResult> Details() 
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var reviews = await _reviewRepository.GetByUserWithAlbumAsync(user.Id);

            AccountDetailsViewModel vm = new AccountDetailsViewModel()
            {
                AppUser = user,
                Reviews = reviews 
            };

            return View(vm);
        }

        public IActionResult Login()
        {
            //response is there for keeping form data from vanishing when reloading page
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Niepoprawne hasło, spróbuj ponownie.";
                return View(loginVM);
            }
            TempData["Error"] = "Nie znaleziono konta o takim adresie e-mail.";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.Email);
            if (user != null)
            {
                TempData["Error"] = "Konto o podanym adresie e-mail już istnieje.";
                return View(registerVM);
            }

            var userN = await _userManager.FindByNameAsync(registerVM.Username);
            if(userN != null)
            {
                TempData["Error"] = "Konto o podanej nazwie użytkownika już istnieje";
                return View(registerVM);
            }

            var tempUser = new AppUser();
            var result = await _passwordValidator.ValidateAsync(_userManager, tempUser, registerVM.Password);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Hasło musi się składać z co najmniej 6 znaków, zawierać co najmniej jedną dużą literę, co najmniej jedną małą literę, co najmniej jedną cyfrę oraz co najmniej jeden znak specjalny.";
                return View(registerVM);
            }

            var newUser = new AppUser()
            {
                UserName = registerVM.Username,
                Email = registerVM.Email,
            };

            
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            else
            {
                var errors = string.Join(", ", newUserResponse.Errors.Select(e => e.Description));
                TempData["Error"] = errors;
                return View(registerVM);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
