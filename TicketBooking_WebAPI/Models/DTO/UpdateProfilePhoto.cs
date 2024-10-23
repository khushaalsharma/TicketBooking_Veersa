using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class UpdateProfilePhoto
    {
        [Required]
        public IFormFile ProfilePhoto { get; set; }
    }
}
