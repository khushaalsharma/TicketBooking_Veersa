using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class NewTicketDTO
    {
        [Required]
        public int TicketQty { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string DateAndTime { get; set; }
        [Required]
        public Guid EventId { get; set; }
    }
}
