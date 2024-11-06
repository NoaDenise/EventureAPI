using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class CategoryCreateEditDTO
    {
        //Q added id for mvc, to test edit
        public  int CategoryId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Namnet måste vara mellan 2 - 100 tecken")]
        public string CategoryName { get; set; }

        [MaxLength(500)]
        public string? CategoryDescription { get; set; }
    }
}

