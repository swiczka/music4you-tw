using music4you.Models;

namespace music4you.Interface
{
    public interface IRatingRepository
    {
        Task<Rating> GetById(int id);
        bool Add(Rating rating);
        bool Update(Rating rating);
        bool Delete(Rating rating);
        bool Save();
    }
}
