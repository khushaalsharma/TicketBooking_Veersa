using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface ITicketTypeRepository
    {
        Task<TicketType> addTicketType(TicketType ticketType);
        Task<List<TicketType>> GetTicketTypesForEvents(Guid eventId);
    }
}
