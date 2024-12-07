using Microsoft.EntityFrameworkCore;
using music4you.Data;
using music4you.Interface;
using music4you.Models;

namespace music4you.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Rating> GetById(int id)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(x => x.Id == id);
            return rating;
        }

        public bool Add(Rating rating)
        {
            _context.Ratings.Add(rating);
            return Save();
        }

        public bool Update(Rating rating)
        {
            _context.Ratings.Update(rating);
            return Save();
        }

        public bool Delete(Rating rating)
        {
            _context.Ratings.Remove(rating);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
