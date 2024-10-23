﻿using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Förnamn måste vara mellan 1-50 tecken")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Förnamn måste vara mellan 1-50 tecken")]
        public string LastName { get; set; }
        public string? UserLocation { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
