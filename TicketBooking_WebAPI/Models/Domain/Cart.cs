using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class Cart
    {
        [Required]
        public Guid Id { get; set; }

        //Foreign Keys
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem> { };
    }
}