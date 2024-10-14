using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class AttendanceShowDTO
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public bool? IsAttending { get; set; }
    }
}
