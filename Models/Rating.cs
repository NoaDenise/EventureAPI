using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        [Range(1, 5)]
        public int? Score { get; set; }

      
       

      
    }
}
