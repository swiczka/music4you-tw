using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using music4you.Interface;
using music4you.Models;

namespace music4you.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly UserManager<AppUser> _userManager;

        public RatingController(IRatingRepository ratingRepository, UserManager<AppUser> userManager)
        {
            _ratingRepository = ratingRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Create(int albumId, int value)
        {
            AppUser appUser;

            if (User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.GetUserAsync(User);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            

            if (appUser != null)
            {
                Rating rating = new Rating()
                {
                    Value = value,
                    AlbumId = albumId,
                    AppUser = appUser
                };

                if (!_ratingRepository.Add(rating))
                {
                    ViewData["Error"] = "Nie udało się dodać oceny albumu";
                }        
                return RedirectToAction("Details", "Album", new { id = albumId });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> Update(int albumId, int ratingId, int value)
        {
            AppUser appUser;

            if (User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.GetUserAsync(User);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            if (appUser != null)
            {
                Rating rating = await _ratingRepository.GetById(ratingId);

                if (rating != null)
                {
                    rating.Value = value;
                    if (!_ratingRepository.Update(rating))
                    {
                        ViewData["Error"] = "Nie udało się dodać oceny albumu";
                    }
                    return RedirectToAction("Details", "Album", new { id = albumId });
                }
                ViewData["Error"] = "Nie udało się dodać oceny albumu";
                return RedirectToAction("Details", "Album", new { id = albumId });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
