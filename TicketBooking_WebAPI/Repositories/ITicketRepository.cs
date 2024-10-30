using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface ITicketRepository
    {
        Task BuyTicket(Ticket ticket);
        Task<bool> CheckTicketQty(Ticket ticket);
        Task<List<Ticket>> GetTicketsByUserId(string userId);
        Task<List<Ticket>> GetTicketsByEventId(Guid eventId);
        Task<bool> DeleteTicket(Guid ticketID, string userId);
    }
}
