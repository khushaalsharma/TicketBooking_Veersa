using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class EventTicketsDTO
    {
        public int TicketQty { get; set; }
        public int Amount { get; set; }
        public string DateAndTime { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
