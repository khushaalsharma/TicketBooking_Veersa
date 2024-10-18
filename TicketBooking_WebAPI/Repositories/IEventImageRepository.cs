using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface IEventImageRepository
    {
        Task<EventImage> AddEventImage(EventImage image);
        Task<List<EventImage>> GetAllEventImages(Guid eventId);
    }
}
