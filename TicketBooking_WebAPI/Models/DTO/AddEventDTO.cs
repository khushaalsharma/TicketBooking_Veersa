using System.ComponentModel.DataAnnotations;
using TicketBooking_WebAPI.Models.DTO;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class AddEventDTO
    {
        [Required]
        [MaxLength(100)]
        public string EventName { get; set; }
        [Required]
        [MaxLength(2500)]
        public string EventDescription { get; set; }
        [Required]
        public string EventVenue { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int MinTicketPrice { get; set; }
        [Required]
        [TicketTypeValidation]
        public List<AddTicketTypeDTO> TicketTypesArr { get; set; }
    }
}

// Validation for Ticket Types Array from the User
public class TicketTypeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var list = value as ICollection<AddTicketTypeDTO>;
        if(list == null || list.Count  == 0)
        {
            return new ValidationResult("The list cannot be empty");
        }

        return ValidationResult.Success;
    }
}
