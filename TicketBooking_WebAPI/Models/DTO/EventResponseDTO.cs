using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class EventResponseDTO
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventVenue { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Category { get; set; }
        public int MinTicketPrice { get; set; }
        public string UserId { get; set; }
        public UserData User { get; set; }
        [Required]
        public List<TicketTypeRespDTO> TicketTypes { get; set; }
        public List<ImageUrlDTO> bannerImg { get; set; }
    }
}
