using System.ComponentModel.DataAnnotations;

namespace TicketBookingWeb_API.Data.DTOs
{
    public class AddEventDTO
    {
        [Required]
        public string EventName { get; set; }
        [Required]
        public string EventVenue { get; set; }
        [Required]
        public string DateAndTime { get; set; }
        [Required]
        public int TicketPrice { get; set; }
        [Required]
        public string EventDescription { get; set; }
    }
}
