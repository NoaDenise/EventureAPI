using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Namnet måste innehålla mellan 2 - 100 tecken" )]
        public string EventName { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 5, ErrorMessage = "Beskrivningen måste vara mellan 5 - 5000 tecken")]
        public string EventDescription { get; set; }
        public DateTime? DateOfEvent { get; set; }
        [Required]
        public string EventLocation { get; set; }

        [StringLength(2000, MinimumLength = 5, ErrorMessage = "Länken måste vara mellan 5 - 2000 tecken")]
        public string? ImageUrl { get; set; }


        [StringLength(2000, MinimumLength = 5, ErrorMessage = "Länken måste vara mellan 5 - 2000 tecken")]
        public string? WebsiteUrl { get; set; }

        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Kontaktinfo måste vara mellan 3 - 2000 tecken")]
        public string? ContactInfo { get; set; }

        [Required]
        public bool IsFree { get; set; }
        [Required]
        public bool Is18Plus { get; set; }
        [Required]
        public bool IsFamilyFriendly { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<EventCategory> EventCategories { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
