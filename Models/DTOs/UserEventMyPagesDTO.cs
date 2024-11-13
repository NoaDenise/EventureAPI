using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EventureAPI.Models.DTOs
{
    public class UserEventMyPagesDTO
    {
        public string ActivityName { get; set; }
        public DateTime? DateOfActivity { get; set; }
        public string ActivityLocation { get; set; }
        public int UserEventId { get; set; }
    }
}
