using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [StringLength(2500)]
        public string? CommentText { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int EventId { get; set; }
        public virtual Event Event { get; set; }

    }
}
