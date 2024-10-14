using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventureAPI.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        
        public bool? IsAttending { get; set; }
    }
}
