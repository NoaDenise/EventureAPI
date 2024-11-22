using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class CommentShowDTO
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string ActivityName { get; set; }
    }
}
