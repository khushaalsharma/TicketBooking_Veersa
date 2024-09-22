using TicketBookingWeb_API.Data.Models;

namespace TicketBookingWeb_API.Repositories
{
    public interface ITicketRepository
    {
        Task<Tickets> BookTicketAsync(Tickets ticket);
    }
}
