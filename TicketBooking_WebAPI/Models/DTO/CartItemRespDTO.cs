using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class CartItemRespDTO
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

        //Navigation
        public Event Event { get; set; }
        public TicketType TicketType { get; set; }
    }
}
