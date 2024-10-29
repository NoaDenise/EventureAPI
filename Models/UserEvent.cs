namespace EventureAPI.Models
{
    public class UserEvent
    {
        public int UserEventId { get; set; }
        public string UserId { get; set; }
        public int ActivityId { get; set; }
    }
}
