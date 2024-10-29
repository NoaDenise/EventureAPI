using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Namnet måste innehålla mellan 2 - 100 tecken" )]
        public string ActivityName { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 5, ErrorMessage = "Beskrivningen måste vara mellan 5 - 5000 tecken")]
        public string ActivityDescription { get; set; }
        public DateTime? DateOfActivity { get; set; }
        [Required]
        public string ActivityLocation { get; set; }

        [StringLength(2000, MinimumLength = 5, ErrorMessage = "Länken måste vara mellan 5 - 2000 tecken")]
        public string? ImageUrl { get; set; }


        [StringLength(2000, MinimumLength = 5, ErrorMessage = "Länken måste vara mellan 5 - 2000 tecken")]
        public string? WebsiteUrl { get; set; }

        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Kontaktinfo måste vara mellan 3 - 2000 tecken")]
        public string? ContactInfo { get; set; }
        [Required]
        public bool IsApproved { get; set; }

        [Required]
        public bool IsFree { get; set; }
        [Required]
        public bool Is18Plus { get; set; }
        [Required]
        public bool IsFamilyFriendly { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<ActivityCategory> ActivityCategories { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
    }
}
