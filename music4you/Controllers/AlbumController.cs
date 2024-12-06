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

        public AlbumController(IAlbumRepository albumRepository) 
        {
            _albumRepository = albumRepository;
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
