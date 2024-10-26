using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class CartItem
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        //Foreign Keys
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public Guid TicketTypeId { get; set; }
        [Required]
        public Guid CartId { get; set; }

        //navigation properties
        public Event Event { get; set; } = new Event();
        public TicketType TicketType { get; set; } = new TicketType();
        public Cart Cart { get; set; }

    }
}
