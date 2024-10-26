using Microsoft.AspNetCore.Identity;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string PreferredLang { get; set; }
        public string PreferredCurr { get; set; }
        public Guid? UserImageId { get; set; }
        public Guid? CartId { get; set; } 
        public UserImage? UserImage { get; set; }
        public Cart? Cart { get; set; }
    }
}
