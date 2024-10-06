using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface ITokenRepository
    {
        string getJwtToken(User user, List<string> roles);
    }
}
