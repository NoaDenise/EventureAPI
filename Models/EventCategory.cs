using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models
{
    public class EventCategory
    {
        [Key]
        public int EventCategoryId { get; set; }

        [Required]
        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
