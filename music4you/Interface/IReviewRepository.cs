using music4you.Models;

namespace music4you.Interface
{
    public interface IReviewRepository
    {
        Task<Review> GetByIdAsync(int id);
        Task<bool> Add(Review review);
        Task<bool> Update(Review review);
        Task<bool> Delete(Review review);
        Task<bool> Save();
        Task<Review> GetByIdWithCommentsAsync(int id);
        Task<List<Review>> GetByUserWithAlbumAsync(string userId);
        Task<Review> GetByIdWithAlbumAsync(int id);
    }
}
