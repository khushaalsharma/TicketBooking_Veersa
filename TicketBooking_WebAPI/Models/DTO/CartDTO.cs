using System.ComponentModel.DataAnnotations;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class CartDTO
    {
        [Required]
        public ICollection<CartItemRespDTO> CartItems { get; set; }
    }
}
