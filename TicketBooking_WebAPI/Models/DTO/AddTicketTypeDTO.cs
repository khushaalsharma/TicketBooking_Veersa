using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class AddTicketTypeDTO
    {
        [Required]
        public string TicketCategory { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int TotalTickets { get; set; }
        [Required]
        public int AvailableTickets { get; set; }
    }
}
