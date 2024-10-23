using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventVenue { get; set; }
        public string Category { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int MinTicketPrice { get; set; }
        public List<ImageUrlDTO> bannerImg { get; set; }
    }
}
