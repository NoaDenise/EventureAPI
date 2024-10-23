using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class CommentEditDTO
    {
        [StringLength(2500)]
        public string? CommentText { get; set; }
    }
}
