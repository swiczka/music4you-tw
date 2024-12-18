using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using music4you.Interface;
using music4you.Migrations;
using music4you.Models;
using music4you.ViewModels;

namespace music4you.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAlbumRepository _albumRepository;
        private readonly ICommentRepository _commentRepository;

        public ReviewController(IReviewRepository reviewRepository, 
                                UserManager<AppUser> userManager, 
                                IAlbumRepository albumRepository,
                                ICommentRepository commentRepository)
        {
            _reviewRepository = reviewRepository;
            _userManager = userManager;
            _albumRepository = albumRepository;
            _commentRepository = commentRepository;
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

        public async Task<IActionResult> Edit(int id)
        {
            AppUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Error", "Error401");
            }

            Review review = await _reviewRepository.GetByIdWithAlbumAsync(id);

            if(user.Id != review.AppUserId)
            {
                return RedirectToAction("Error", "Error403");
            }

            ReviewEditViewModel vm = new ReviewEditViewModel()
            {
                Id = review.Id,
                AlbumId = review.AlbumId,
                Album = review.Album,
                Content = review.Content,
                Title = review.Title,
                Value = review.Value,
                CreatedAt = review.CreatedAt
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReviewEditViewModel vm)
        {
            if(!ModelState.IsValid) 
            {
                return View(vm);
            }

            Review review = await _reviewRepository.GetByIdAsync(vm.Id);

            review.Content = vm.Content;
            review.Title = vm.Title;
            review.Value = vm.Value;
            review.Id = vm.Id;

            await _reviewRepository.Update(review);
            return RedirectToAction("Details", "Account");
        }

        public async Task<IActionResult> Delete(int id)
        {
            AppUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Error401", "Error");
            }

            Review review = await _reviewRepository.GetByIdWithCommentsAsync(id);

            if(review == null)
            {
                return RedirectToAction("Error404", "Error");
            }
            if (user.Id != review.AppUserId)
            {
                return RedirectToAction("Error403", "Error");
            }

            List<Comment> comments = review.Comments.ToList();

            if(comments.Count > 0)
            {
                foreach (Comment comment in comments)
                {
                    _commentRepository.Delete(comment);
                }
            }
            

            _reviewRepository.Delete(review);
            return RedirectToAction("Details", "Account");
        }

        public async Task<IActionResult> Details(int id)
        {
            Review review = await _reviewRepository.GetByIdWithCommentsAsync(id);
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
