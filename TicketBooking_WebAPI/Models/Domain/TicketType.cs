using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class TicketType
    {
        public Guid Id { get; set; }
        public string TicketCategory { get; set; }
        public int Price { get; set; }
        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }
        [ForeignKey(nameof(Event))]
        public Guid EventId { get; set; }

        //navigation
        public Event Event { get; set; }
    }
}
