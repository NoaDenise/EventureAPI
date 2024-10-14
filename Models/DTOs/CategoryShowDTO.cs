using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class CategoryShowDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Namnet måste vara mellan 2 - 100 tecken")]
        public string CategoryName { get; set; }

        [MaxLength(500)]
        public string? CategoryDescription { get; set; }
    }
}
