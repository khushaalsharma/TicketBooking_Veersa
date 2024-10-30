using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class Payments
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string MethodDetail { get; set; } //takes card number
        [Required]
        public DateTime BoughtAt { get; set; }
        [Required]      
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        //navigation
        public User User { get; set; }
    }
}
