using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public int TicketQty { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string DateAndTime { get; set; }
        
        //Foreign keys
        public Guid EventId { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public Guid TicketTypeId { get; set; }
        public Guid PaymentsId { get; set; }

        //navigation 
        public Event Event { get; set; }
        public User User { get; set; }
        public TicketType TicketType { get; set; }
        public Payments Payments { get; set; }

    }
}
