using Microsoft.AspNetCore.Identity;

namespace TicketBookingWeb_API.Data.Models
{
    public class UserData : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
