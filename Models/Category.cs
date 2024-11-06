using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventureAPI.Models
{
    public class Category
    {
        [Key]
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Namnet måste vara mellan 2 - 100 tecken")]
        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }

        [MaxLength(500)]
        [JsonPropertyName("categoryDescription")]
        public string? CategoryDescription { get; set; }
        public virtual ICollection<ActivityCategory> ActivityCategories { get; set; }
        public virtual ICollection<UserCategory> UserCategories { get; set; }
    }
}
