using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;

namespace EventureAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddCommentAsync(CommentCreateEditDTO commentDto)
        {
            var newComment = new Comment
            {
                UserId = commentDto.UserId,
                ActivityId = commentDto.ActivityId,
                CommentText = commentDto.CommentText,
                CreatedAt = DateTime.Now,
            };
            
            await _commentRepository.AddCommentAsync(newComment);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            await _commentRepository.DeleteCommentAsync(commentId);
        }

        public async Task EditCommentAsync(int commentId, CommentEditDTO commentDto)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            comment.CommentText = commentDto.CommentText;
            
            await _commentRepository.EditCommentAsync(comment);
        }

        public async Task<IEnumerable<CommentShowDTO>> GetAllCommentsByActivityAsync(int activityId)
        {
            var comments = await _commentRepository.GetAllCommentsByActitivityAsync(activityId);

            var commentDtos = comments.Select(comment => new CommentShowDTO
            {
                CommentText = comment.CommentText,
                CreatedAt = comment.CreatedAt,
                UserName = comment.User?.UserName ?? "Unknown",
                ActivityName = comment.Activity?.ActivityName ?? "Unknown activity", 
            }).ToList();

            return commentDtos;
        }

        public async Task<CommentShowDTO> GetCommentByIdAsync(int commentId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            var commentDto = new CommentShowDTO
            {
                CommentText = comment.CommentText,
                CreatedAt = comment.CreatedAt,
                UserName = comment.User.UserName,
                ActivityName = comment.Activity.ActivityName,
            };

            return commentDto;
        }
    }
}
