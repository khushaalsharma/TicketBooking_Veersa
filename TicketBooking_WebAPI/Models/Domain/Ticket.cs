using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public int TicketQty { get; set; }
        public int Amount { get; set; }
        public string DateAndTime { get; set; }
        
        //Foreign keys
        public Guid EventId { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public Guid TicketTypeId { get; set; }

        //navigation 
        public Event Event { get; set; }
        public User User { get; set; }
        public TicketType TicketType { get; set; }

    }
}
