using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class EventImageUploadDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid EventId { get; set; }
    }
}
