using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class BuyTicketDTO
    {
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
        [TicketValidation]
        public List<TicketData> NewTickets { get; set; }
    }

    public class TicketData
    {
        [Required]
        public int TicketQty { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        //Foreign keys
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public Guid TicketTypeId { get; set; }

    }
}

public class TicketValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var list = value as ICollection<TicketData>;
        if (list == null || list.Count == 0)
        {
            return new ValidationResult("The list cannot be empty");
        }

        return ValidationResult.Success;
    }
}
