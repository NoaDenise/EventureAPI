using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventureAPI.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly EventureContext _context;
        public CommentRepository(EventureContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
        }

        public async Task EditCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByActitivityAsync(int actitivtyId)
        {
            var comments = await _context.Comments
                .Include(c => c.Activity)
                .Include(c => c.User)
                .Where(c => c.Activity.ActivityId == actitivtyId)
                .ToListAsync();
               
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            var comment = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Activity)
                .FirstOrDefaultAsync(c => c.CommentId == commentId);

            if (comment == null)
            {
                throw new Exception("Comment not found.");
            }

            return comment;
        }
    }
}
