using music4you.Models;

namespace music4you.Interface
{
    public interface ICommentRepository
    {
        bool Add(Comment comment);
        bool Update(Comment comment);
        bool Delete(Comment comment);
        bool Save();
    }
}
