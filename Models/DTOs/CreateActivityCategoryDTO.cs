using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class CreateActivityCategoryDTO
    {
        public int ActivityId { get; set; }

        public int CategoryId { get; set; }

    }
}