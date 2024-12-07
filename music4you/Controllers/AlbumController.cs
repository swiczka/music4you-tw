using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using music4you.Data;
using music4you.Interface;
using music4you.Models;
using music4you.Repository;
using music4you.ViewModels;

namespace music4you.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly UserManager<AppUser> _userManager;

        public AlbumController(IAlbumRepository albumRepository, UserManager<AppUser> userManager) 
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
            Album album = await _albumRepository.GetById(id);
            string userId = _userManager.GetUserId(User);

            if(userId != null)
            {
                Rating userRating = await _albumRepository.GetUserRating(id, userId);
                AlbumViewModel vm = new AlbumViewModel()
                {
                    Id = id,
                    Name = album.Name,
                    Author = album.Author,
                    Year = album.Year,
                    Genre = album.Genre,
                    ImageUrl = album.ImageUrl,
                    Ratings = album.Ratings.ToList(),
                    UserRating = userRating
                };
                return View(vm);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            

            
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AlbumCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
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
    }
}
