using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models
{
    public class UserEvent
    {
        [Key]
        public int UserEventId { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

    }
}
