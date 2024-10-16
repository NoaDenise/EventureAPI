﻿using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class ActivityCreateEditDTO
    {
        public int ActivityId { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Namnet måste innehålla mellan 2 - 100 tecken")]
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
    }
}
