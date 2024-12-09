using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using music4you.Interface;
using music4you.Models;
using music4you.ViewModels;

namespace music4you.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Create(ReviewViewModel vm)
        {
            AppUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "Treść jest niepoprawna";
                return RedirectToAction("Details", "Review", new { id = vm.Id });
            }

            Comment comment = new Comment()
            {
                ReviewId = vm.Id,
                AppUserId = user.Id,
                Content = vm.Comment,
                CreatedDate = DateTime.Now
            };
            if (_commentRepository.Add(comment))
            {
                return RedirectToAction("Details", "Review", new { id = vm.Id });
            }
            else
            {
                ViewData["Error"] = "Nie udało się dodać komentarza do bazy";
                return RedirectToAction("Details", "Review", new { id = vm.Id });
            }
        }
    }
}
