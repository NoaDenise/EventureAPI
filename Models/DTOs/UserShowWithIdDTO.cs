using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class UserShowWithIdDTO
    {
        //do we need this dto - added id in usershowDTO?
        
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserLocation { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserCategories { get; set; }
    }
}
