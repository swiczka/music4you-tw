using Microsoft.EntityFrameworkCore;
using music4you.Data;
using music4you.Interface;
using music4you.Models;

namespace music4you.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _context;

        public AlbumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Album>> GetAll()
        {
            var albums = await _context.Albums.ToListAsync();
            return albums;
        }

        public async Task<IEnumerable<Album>> GetFilteredByName(string name)
        {
            var albums = await _context.Albums.Where(a => a.Name.Contains(name)).ToListAsync();
            return albums;
        }

        public async Task<Rating> GetUserRating(int albumId, string userId)
        {
            var rating = await _context.Ratings
                .FirstOrDefaultAsync(a => a.AlbumId == albumId && a.AppUserId == userId);
            return rating;
        }

        public async Task<Album> GetById(int id)
        {
            var album = await _context.Albums
                .FirstOrDefaultAsync(x => x.Id == id);
            return album;
        }

        public async Task<Album> GetByIdExtended(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Ratings)
                .Include(b => b.Reviews)
                    .ThenInclude(c => c.AppUser)
                .FirstOrDefaultAsync(x => x.Id == id);
            return album;
        }

        public bool Add(Album album)
        {
            _context.Albums.Add(album);
            return Save();
        }

        public bool Update(Album album)
        {
            _context.Albums.Update(album);
            return Save();
        }

        public bool Delete(Album album)
        {
            _context.Albums.Remove(album);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
