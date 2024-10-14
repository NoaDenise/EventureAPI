using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class RatingCreateEditDTO
    {

        //[Required]
        //public string UserId { get; set; }

        [Required]
        public int ActivityId { get; set; }

        [Range(1, 5)]
        public int? Score { get; set; }
    }
}
