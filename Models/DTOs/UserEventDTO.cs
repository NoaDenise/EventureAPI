using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class UserEventDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ActivityId { get; set; }

    }
}
