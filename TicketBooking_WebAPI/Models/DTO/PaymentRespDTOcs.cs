using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class PaymentRespDTOcs
    {
        public Guid Id { get; set; }
        public DateTime BoughtAt { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}
