using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models
{
    public class UserCategory
    {
        [Key]
        public int UserCategoryId { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
