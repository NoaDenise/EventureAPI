using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class CommentCreateEditDTO
    {
        [StringLength(2500)]
        public string? CommentText { get; set; }
        public DateTime CreatedAt { get; set; }

        //[Required]
        //public string UserId { get; set; }

        //[Required]
        //public int ActivityId { get; set; }
    }
}
