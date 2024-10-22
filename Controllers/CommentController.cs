using EventureAPI.Models.DTOs;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("addComment")]
        public async Task<ActionResult> AddComment(CommentCreateEditDTO comment)
        {
            await _commentService.AddCommentAsync(comment);
            return Ok($"Added new comment at: {comment.CreatedAt}");
        }

        [HttpDelete("deleteComment/{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);
            return Ok($"Comment with id:{commentId} has been deleted");
        }

        [HttpPut("editComment/{commentId}")]
        public async Task<ActionResult> EditComment(int commentId, CommentEditDTO updatedComment)
        {
            await _commentService.EditCommentAsync(commentId, updatedComment);
            return Ok();
        }

        [HttpGet("getAllCommentsByActivity/{activityId}")]
        public async Task<ActionResult> GetAllCommentsByActivity(int activityId)
        {
            var comments = await _commentService.GetAllCommentsByActivityAsync(activityId);
            return Ok(comments);
        }

        [HttpGet("getCommentById/{commentId}")]
        public async Task<ActionResult> GetCommentById(int commentId)
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            return Ok(comment);
        }
    }
}
