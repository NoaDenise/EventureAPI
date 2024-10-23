using EventureAPI.Models.DTOs;

namespace EventureAPI.Services.IServices
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentShowDTO>> GetAllCommentsByActivityAsync(int activityId);
        Task AddCommentAsync(CommentCreateEditDTO commentDto);
        Task EditCommentAsync(int commentId, CommentEditDTO commentDto);
        Task DeleteCommentAsync(int commentId);
        Task<CommentShowDTO> GetCommentByIdAsync(int commentId);
    }
}
