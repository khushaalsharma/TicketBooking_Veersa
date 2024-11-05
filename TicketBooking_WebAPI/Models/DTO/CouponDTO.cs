using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class CouponDTO
    {
        [Required]
        [MaxLength(10)]
        [RegularExpression("^[A-Z0-9]+$", ErrorMessage = "The code can have only upper case, numbers and maximum 10 characters")]
        public string Code { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }
        [Required]
        public int UsesLeft { get; set; }
        [Required]
        public DateTime LastDate { get; set; }
    }
}
