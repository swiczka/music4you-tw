using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using music4you.Interface;
using music4you.Models;
using music4you.ViewModels;

namespace music4you.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAlbumRepository _albumRepository;

        public ReviewController(IReviewRepository reviewRepository, UserManager<AppUser> userManager, IAlbumRepository albumRepository)
        {
            _reviewRepository = reviewRepository;
            _userManager = userManager;
            _albumRepository = albumRepository;
        }

        public async Task<IActionResult> Create(int albumId)
        {
            AppUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Album album = await _albumRepository.GetById(albumId);

            ReviewCreateViewModel vm = new ReviewCreateViewModel()
            {
                Album = album
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateViewModel vm)
        {
            AppUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                Review review = new Review()
                {
                    Title = vm.Title,
                    Content = vm.Content,
                    Value = vm.Value,
                    AlbumId = vm.AlbumId,
                    AppUserId = user.Id,
                    CreatedAt = DateTime.Now
                };
                if(await _reviewRepository.Add(review))
                {
                    return RedirectToAction("Details", "Album", new { id = vm.AlbumId });
                }
                
            }
            Album album = await _albumRepository.GetById(vm.AlbumId);
            vm.Album = album;
            return View(vm);

        }

        public async Task<IActionResult> Details(int id)
        {
            Review review = await _reviewRepository.GetByIdWithComments(id);
            ReviewViewModel vm = new ReviewViewModel()
            {
                Id = id,
                Title = review.Title,
                Content = review.Content,
                Value = review.Value,
                Album = review.Album,
                AlbumId= review.AlbumId,
                CreatedAt = review.CreatedAt,
                AppUser = review.AppUser,
                Comments = review.Comments.ToList()
            };
            return View(vm);
        }
    }
}
