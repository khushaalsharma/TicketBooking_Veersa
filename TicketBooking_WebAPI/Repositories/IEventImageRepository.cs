using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface IEventImageRepository
    {
        Task<EventImage> AddEventImage(EventImage image);
        Task<List<EventImage>> GetAllEventImages(Guid eventId);
        Task<UserImage> AddProfileImage(UserImage image, string userId);
        Task<bool> DeleteProfileImage(Guid imageId, string userId);
    }
}
