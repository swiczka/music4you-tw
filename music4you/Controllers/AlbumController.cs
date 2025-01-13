using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using music4you.Data;
using music4you.Interface;
using music4you.Models;
using music4you.Repository;
using music4you.ViewModels;
using System.Text;

namespace music4you.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly UserManager<AppUser> _userManager;

        public AlbumController(IAlbumRepository albumRepository,
                                UserManager<AppUser> userManager) 
        {
            _albumRepository = albumRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var albums = await _albumRepository.GetAll();
                return View(albums);
            }
            else
            {
                var albums = await _albumRepository.GetFilteredByName(search);
                return View(albums);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            Album album = await _albumRepository.GetByIdExtended(id);

            if(album == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            string userId = _userManager.GetUserId(User);

            if(userId != null)
            {
                Rating userRating = await _albumRepository.GetUserRating(id, userId);
                var review = await _albumRepository.GetAlbumUserReview(userId, album.Id);

                bool isReviewed = (review != null);

                AlbumViewModel vm = new AlbumViewModel()
                {
                    Id = id,
                    Name = album.Name,
                    Author = album.Author,
                    Year = album.Year,
                    Genre = album.Genre,
                    ImageUrl = album.ImageUrl,
                    Ratings = album.Ratings.ToList(),
                    Reviews = album.Reviews.ToList(),
                    UserRating = userRating,
                    IsReviewed = isReviewed
                };
                return View(vm);
            }
            else
            {
                AlbumViewModel vm = new AlbumViewModel()
                {
                    Id = id,
                    Name = album.Name,
                    Author = album.Author,
                    Year = album.Year,
                    Genre = album.Genre,
                    ImageUrl = album.ImageUrl,
                    Ratings = album.Ratings.ToList(),
                    Reviews = album.Reviews.ToList(),
                    IsReviewed = false
                };
                return View(vm);
            }
        }

        public async Task<IActionResult> Create()
        {
            AppUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            AlbumCreateViewModel vm = new AlbumCreateViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumCreateViewModel vm)
        {
            AppUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                if(!(await ValidateImageAsync(vm.ImageUrl)))
                {
                    TempData["Error"] = "Podaj poprawny link do obrazka. Najczęściej kończy się na '.jpg', '.png', itd..";
                    return View(vm);
                }
                Album newAlbum = new Album()
                {
                    Name = vm.Name,
                    Author = vm.Author,
                    Year = vm.Year,
                    Genre = vm.Genre,
                    ImageUrl = vm.ImageUrl
                };

                if (_albumRepository.Add(newAlbum))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Error"] = "Nie powiodło się dodawanie albumu";
                    return View(vm);
                }

                
            }
            else
            {
                return View(vm);
            }
        }

        private async Task<bool> ValidateImageAsync(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Head, imageUrl);
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var contentType = response.Content.Headers.ContentType.MediaType;

                        if (contentType.StartsWith("image/"))
                        {
                            return true;
                        }
                    }
                }
                catch
                {
                }
            }

            return false;
        }
    }
}
