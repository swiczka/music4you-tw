using music4you.Models;

namespace music4you.Interface
{
    public interface IReviewRepository
    {
        Task<Review> GetById(int id);
        Task<bool> Add(Review review);
        Task<bool> Update(Review review);
        Task<bool> Delete(Review review);
        Task<bool> Save();
    }
}
