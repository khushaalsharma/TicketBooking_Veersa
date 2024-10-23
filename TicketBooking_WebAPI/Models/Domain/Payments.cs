using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class Payments
    {
        public Guid Id { get; set; }
        public string PaymentMethod { get; set; }
        public string MethodDetail { get; set; } //takes card number or apple ID
        //public DateTime CreatedAt { get; set; }
        public int Amount { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        //navigation
        public User User { get; set; }
    }
}
