using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class AttendanceCreateEditDTO
    {
        public string UserId { get; set; }
        public int ActivityId { get; set; }
        public bool? IsAttending { get; set; }
    }
}
