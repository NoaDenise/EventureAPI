using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Förnamn måste vara mellan 1-50 tecken")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Förnamn måste vara mellan 1-50 tecken")]
        public string LastName { get; set; }
        public string? UserLocation { get; set; }
        public ICollection<UserCategory> UserCategories { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Event> Events { get; set; }

    }
}
