using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class AddEventDTO
    {
        [Required]
        public string EventName { get; set; }
        [Required]
        public string EventDescription { get; set; }
        [Required]
        public string DateAndTime { get; set; }
        [Required]
        public string EventVenue { get; set; }
        [Required]
        public int TicketPrice { get; set; }
        [Required]
        public int TotalTickets { get; set; }
        [Required]
        public int AvailableTickets { get; set; }
    }
}
