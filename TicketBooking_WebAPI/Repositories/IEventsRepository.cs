using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface IEventsRepository
    {
        Task<List<Event>> GetAllEvents(string? name, string? venue, string? category, DateOnly? From, DateOnly? To, string? sortBy, bool isAsc = true);
        Task<List<Event>?> GetEventsByUserId(string userId);
        Task<Event> GetEventById(Guid eventId);
        Task<Event> AddEvent(Event newEvent);
    }
}
