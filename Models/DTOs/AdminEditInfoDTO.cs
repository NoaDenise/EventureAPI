using System.ComponentModel.DataAnnotations;

namespace EventureAPI.Models.DTOs
{
    public class AdminEditInfoDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
