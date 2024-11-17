namespace EventureAPI.Models.DTOs
{
    public class UserEditInfoDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserLocation { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
