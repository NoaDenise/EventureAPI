using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class ActivityCategoryShowDTO
    {
        public int ActivityCategoryId { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
