using TicketBookingWeb_API.Data.Models;

namespace TicketBookingWeb_API.Repositories
{
    public interface IEventsRepository
    {
        Task<List<Events>> GetAllAsync();
        Task<Events> AddEventAsync(Events program);
    }
}
