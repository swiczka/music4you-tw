using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using music4you.Data;
using music4you.Interface;
using music4you.Models;

namespace music4you.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReviewRepository(ApplicationDbContext context, UserManager<AppUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> Add(Review review)
        {
            _context.Reviews.Add(review);
            return await Save();
        }

        public async Task<bool> Delete(Review review)
        {
            _context.Reviews.Remove(review);
            return await Save();
        }

        public async Task<Review> GetById(int id)
        {
            Review review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            return review;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Review review)
        {
            _context.Reviews.Update(review);
            return await Save();
        }
    }
}
