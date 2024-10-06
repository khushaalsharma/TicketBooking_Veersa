using Microsoft.AspNetCore.Identity;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
