using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models
{
    public class ActivityCategory
    {
        [Key]
        public int ActivityCategoryId { get; set; }

        [Required]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
