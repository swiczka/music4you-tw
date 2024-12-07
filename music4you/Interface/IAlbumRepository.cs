using music4you.Models;

namespace music4you.Interface
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetAll();
        Task<Album> GetById(int id);
        bool Add(Album album);
        bool Update(Album album);
        bool Delete(Album album);
        bool Save();
        Task<IEnumerable<Album>> GetFilteredByName(string name);
        Task<Rating> GetUserRating(int albumId, string userId);
    }
}
