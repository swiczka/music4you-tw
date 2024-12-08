using music4you.Data;
using music4you.Interface;
using music4you.Models;

namespace music4you.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool Add(Comment comment)
        {
            _context.Comments.Add(comment);
            return Save();
        }

        public bool Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Comment comment)
        {
            _context.Comments.Update(comment);
            return Save();
        }
    }
}
