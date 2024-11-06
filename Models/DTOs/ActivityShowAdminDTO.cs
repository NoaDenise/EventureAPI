namespace EventureAPI.Models.DTOs
{
    public class ActivityShowAdminDTO
    {
        public int ActivityId { get; set; }

        public string UserId { get; set; }

        public string ActivityName { get; set; }

        public string ActivityDescription { get; set; }
        public DateTime? DateOfActivity { get; set; }
        public string ActivityLocation { get; set; }

        public string? ImageUrl { get; set; }

        public string? WebsiteUrl { get; set; }

        public string? ContactInfo { get; set; }
        public bool IsApproved { get; set; }

        public bool IsFree { get; set; }
        public bool Is18Plus { get; set; }

        public bool IsFamilyFriendly { get; set; }
    }
}