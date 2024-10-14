using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class RatingShowDTO
    {
        public string ActivityName { get; set; }
        public int? Score { get; set; }
    }
}
