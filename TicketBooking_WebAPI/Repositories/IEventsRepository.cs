using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface IEventsRepository
    {
        Task<List<Event>> GetAllEvents();
        Task<List<Event>?> GetEventsByUserId(string userId);
        Task<Event> AddEvent(Event newEvent);
    }
}
