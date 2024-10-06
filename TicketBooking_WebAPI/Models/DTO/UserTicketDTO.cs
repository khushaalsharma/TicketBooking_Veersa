using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class UserTicketDTO
    {
        public int TicketQty { get; set; }
        public int Amount { get; set; }
        public string DateAndTime { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
